using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreLayer.Entities.TodoItem;

namespace CoreLayer.DTOs.TodoDTO
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Iscompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public TodoItem.Priority priorities{ get; set; }
     
    }
}
