using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DeletedTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CraetedTime { get; set; }
        public List<TodoItem>TodoItemsList { get; set; }
       

    }
}
