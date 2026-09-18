using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreLayer.Entities.TodoItem;

namespace CoreLayer.DTOs.TodoDTO
{
    public class CreateTodoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority priorities { get; set; }
    }
}
