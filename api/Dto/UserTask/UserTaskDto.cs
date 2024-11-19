using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto.UserTask
{
    public class UserTaskDto
    {
        public int Id {get; set;}
        [Required]
        [MinLength(1, ErrorMessage = "name cannot be empty") ]
        public string name {get; set;} = string.Empty;
        public int? CategoryId {get; set;}
        public DateTime CreatedOn {get; set;} = DateTime.Now;
        [Required]
        public Boolean status {get; set;} = false;
    }
}