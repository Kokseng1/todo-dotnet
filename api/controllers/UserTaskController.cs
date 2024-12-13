using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.UserTask;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/UserTask")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserTaskRepositoryInterface _userTaskRepository;
        private readonly CategoryRepositoryInterface _categoryRepositoryInterface;
        public UserTaskController(ApplicationDBContext context, UserTaskRepositoryInterface userTaskRepository, CategoryRepositoryInterface categoryRepositoryInterface)
        {
            _categoryRepositoryInterface = categoryRepositoryInterface;
            _context = context;
            _userTaskRepository = userTaskRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var userTasks = await _userTaskRepository.GetAllAsync(queryObject);
            var userTaskDto = userTasks.Select(task => task.ToUserTaskDto());
            return Ok(userTaskDto);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userTask = await _userTaskRepository.GetByIdAsync(id);
            // var userTask = await _context.UserTasks
                // .Include(ut => ut.Category) // Eagerly load Category
                // .FirstOrDefaultAsync(ut => ut.Id == id);
            // Console.WriteLine("in here");
            // Console.WriteLine(userTask);
            // Console.WriteLine(userTask.Category.Name);
            if(userTask == null)
            {
                return NotFound(); //a type of Iaactionresult
            }

            return Ok(userTask.ToUserTaskDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTaskUsingCategoryName([FromBody] CreateUserTaskRequestDto createUserTaskDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var category = await _categoryRepositoryInterface.GetByNameAsync(createUserTaskDto.categoryName);
            if (category == null) {
                return BadRequest("invalid category name");
            }
            var categoryId = category.Id;
     
            var userTask = createUserTaskDto.ToUserTaskFromRequestDto(categoryId);
            await _userTaskRepository.CreateAsync(userTask);
            return CreatedAtAction(nameof(GetById), new { id = userTask.Id }, userTask.ToUserTaskDto());
        }

        [HttpPost("{categoryId}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int categoryId, [FromBody] CreateUserTaskRequestDto createUserTaskDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (!await _categoryRepositoryInterface.CategoryExists(categoryId)) {
                return BadRequest("Category doees not exist");
            }
            var userTask = createUserTaskDto.ToUserTaskFromRequestDto(categoryId);
            await _userTaskRepository.CreateAsync(userTask);
            return CreatedAtAction(nameof(GetById), new { id = userTask.Id }, userTask.ToUserTaskDto());
        }


        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserTaskDto updateUserTaskDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var userTask = await _userTaskRepository.UpdateAsync(id, updateUserTaskDto); 
            if (userTask == null) {
                return NotFound();
            }
            return Ok(userTask.ToUserTaskDto());
        } 

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var userTask = await _userTaskRepository.DeleteAsync(id);

            if (userTask == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}