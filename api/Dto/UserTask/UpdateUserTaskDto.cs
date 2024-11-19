using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dto.UserTask
{
    public class UpdateUserTaskDto
    {
        public string name {get; set;} = string.Empty;
        public int? CategoryId {get; set;}
        public DateTime CreatedOn {get; set;} = DateTime.Now;

        public Boolean status {get; set;} = false;
    }
}