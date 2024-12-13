using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto.UserTask
{
    public class CreateUserTaskRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "name must not be empty")]
        public string name {get; set;} = string.Empty;
        public DateTime CreatedOn {get; set;} = DateTime.Now;

        public Boolean status {get; set;} = false;

        public String categoryName {get; set;} = "";
    }
}