using CoreLayer.Entities;
using DataLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs.TodoDTO
{
    public class TodoFilterDTO
    {
        public TodoItem.Priority? Priority { get; set; }

        public bool? IsCompleted { get; set; }

        public string? SearchText { get; set; }

        public string? Sort { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}
