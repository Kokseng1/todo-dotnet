using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Category
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
         public List<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}