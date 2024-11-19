using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.UserTask;
using api.Models;

namespace api.Dto.Category
{
    public class CategoryDto
    {
        public string name {get; set;} = string.Empty;
        public List<UserTaskDto> Tasks {get; set;}
    }
}