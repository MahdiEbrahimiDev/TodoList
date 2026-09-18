using CoreLayer.DTOs.TodoDTO;
using CoreLayer.Entities;
using CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace CoreLayer.Services.TodoServices
{
    public interface ITodoServices
    {
        PaginationDTO<TodoDTO> GetAllTodo(int UserId, TodoFilterDTO filter);
        public TodoItem GetById(int id, int userId);
        public resultOperation CreateTodo(CreateTodoDTO DTO,int UserId);
        public resultOperation UpdateTodo(UpdateTodoDTO DTO, int userId);
        public resultOperation CompletedTodo(int Id, int userId);
        public resultOperation DeleteTodo(int id, int userId);
    }
}
