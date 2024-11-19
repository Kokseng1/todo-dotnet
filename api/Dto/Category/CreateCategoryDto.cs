using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "name must not be empty")]
        public string name {get; set;} = string.Empty;
    }
}