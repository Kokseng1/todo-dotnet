using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class UserTask
    {
        public int Id {get; set;}
        public string name {get; set;} = string.Empty;
        public int? CategoryId {get; set;}
        public Category? Category {get; set;}

        public Boolean status {get; set;} = false;

        public DateTime CreatedOn {get; set;} = DateTime.Now;
    }
}