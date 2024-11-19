using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.UserTask;
using api.Models;

namespace api.Dto.Category
{
    public class CategoryDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "name must not be empty")]
        public string name {get; set;} = string.Empty;
        public List<UserTaskDto> Tasks {get; set;}
    }
}