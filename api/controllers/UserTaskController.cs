using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.UserTask;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [Route("api/UserTask")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserTaskRepository _userTaskRepository;
        public UserTaskController(ApplicationDBContext context, UserTaskRepository userTaskRepository)
        {
            _context = context;
            _userTaskRepository = userTaskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userTasks = await _userTaskRepository.GetAllAsync();
            var userTaskDto = userTasks.Select(task => task.ToUserTaskDto());
            return Ok(userTaskDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userTask = await _userTaskRepository.GetByIdAsync(id);

            if(userTask == null)
            {
                return NotFound(); //a type of Iaactionresult
            }

            return Ok(userTask.ToUserTaskDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserTaskRequestDto createUserTaskDto) {
            var userTask = createUserTaskDto.ToUserTaskFromRequestDto();
            await _userTaskRepository.CreateAsync(userTask);
            return CreatedAtAction(nameof(GetById), new { id = userTask.Id }, userTask.ToUserTaskDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserTaskDto updateUserTaskDto) {
            var userTask = await _userTaskRepository.UpdateAsync(id, updateUserTaskDto); 
            if (userTask == null) {
                return NotFound();
            }
            return Ok(userTask.ToUserTaskDto());
        } 

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var userTask = await _userTaskRepository.DeleteAsync(id);

            if (userTask == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}