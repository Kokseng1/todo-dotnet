using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMapper
    {
          public static CategoryDto ToCategoryDto(this Category category) {
            return new CategoryDto {
                name = category.Name,
                Tasks = category.Tasks.Select(t => t.ToUserTaskDto()).ToList()
            };
        }

        public static Category ToCategoryFromRequestDto(this CreateCategoryDto createCategoryDto)
        {
            return new Category {
                Name = createCategoryDto.name
            };
        }
    }
}