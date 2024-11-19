using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Category;
using api.Dto.UserTask.Category;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [Route("apli/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepositoryInterface _categoryReporitory;
        public CategoryController(CategoryRepositoryInterface categoryReporitory)
        {
            _categoryReporitory = categoryReporitory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryReporitory.GetAllAsync();
            var categoryDto = categories.Select(category => category.ToCategoryDto());
            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _categoryReporitory.GetByIdAsync(id);

            if(category == null)
            {
                return NotFound(); //a type of Iaactionresult
            }

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto) {
            var category = createCategoryDto.ToCategoryFromRequestDto();
            await _categoryReporitory.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category.ToCategoryDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto UudateCategoryDto) {
            var category = await _categoryReporitory.UpdateAsync(id, UudateCategoryDto); 
            if (category == null) {
                return NotFound();
            }
            return Ok(category.ToCategoryDto());
        } 

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var category = await _categoryReporitory.DeleteAsync(id);

            if (category == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}