using CoreLayer.Entities;
using CoreLayer.Services.UserServices;
using CoreLayer.Utilities;
using System;
using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CoreLayer.DTOs.TodoDTO;
using DataLayer.Migrations;
using Microsoft.IdentityModel.Tokens;

namespace CoreLayer.Services.TodoServices
{
    
    public class TodoServices : ITodoServices
    {
        private readonly DataLayer.Context.AppDB _Context;
        public TodoServices(DataLayer.Context.AppDB Context)
        {
            _Context= Context;
        }

        public resultOperation CompletedTodo(int Id,int UserId)
        {
            var findTodo = _Context.TodoItems
       .FirstOrDefault(x => x.Id == Id && x.UserId==UserId&&!x.IsDeleted);

            if (findTodo == null)
            {
                return resultOperation.Error("تسک مورد نظر پیدا نشد.");
            }

            findTodo.isCompleted = !findTodo.isCompleted;

            _Context.SaveChanges();

            return resultOperation.Success(
                findTodo.isCompleted
                    ? "تسک با موفقیت تکمیل شد."
                    : "تسک از حالت تکمیل خارج شد."
            );

        }

        public resultOperation CreateTodo(CreateTodoDTO item,int UserId)
        {
            var user = _Context.Users
    .FirstOrDefault(x => x.Id == UserId && !x.IsDeleted);

            if (user == null)
            {
                return resultOperation.Error("کاربر یافت نشد.");
            }
            var todo = new TodoItem
            {
                CreatedDate = DateTime.Now,
                Name = item.Title,
                isCompleted = false,
                IsDeleted = false,
                Description= item.Description,
                UserId= UserId,
                Priorities=item.priorities,
                
            };
            try
            {
                _Context.TodoItems.Add(todo);
                var res = _Context.SaveChanges();
                return resultOperation.Success("با موفقیت ثبت شد");
            }
            catch (Exception e)
            {

                return resultOperation.Error(e.Message);
            }
           
        }

       

        public resultOperation DeleteTodo(int id, int userId)
        {
            var foundItem = GetById(id, userId);

            if (foundItem == null)
            {
                return resultOperation.Error("آیتم مورد نظر یافت نشد");
            }

            foundItem.DeletedTime = DateTime.Now;
            foundItem.IsDeleted = true;

            _Context.SaveChanges();

            return resultOperation.Success("آیتم مورد نظر با موفقیت حذف شد");
        }

        public PaginationDTO<TodoDTO> GetAllTodo(int UserId,TodoFilterDTO filter)
        {
            var query = _Context.TodoItems
                .Where(x =>
                    x.UserId == UserId &&
                    !x.IsDeleted);
            if (filter.Priority.HasValue)
            {
                 query=query.Where(c => c.Priorities == filter.Priority);
            }

            if (!String.IsNullOrWhiteSpace(filter.SearchText))
            {
                var trimtext=filter.SearchText.Trim();
             query=query.Where(c=>c.Name.Contains(trimtext)||
             c.Description.Contains(trimtext));
            }
            if (filter.IsCompleted.HasValue)
            {
                query = query.Where(c =>
                    c.isCompleted == filter.IsCompleted.Value);
            }
            if (String.IsNullOrWhiteSpace(filter.Sort))
            {
                query = query.OrderByDescending(x => x.CreatedDate);
               
            }
            else
            {
                switch (filter.Sort)
                {
                    case "Newest":
                        query = query.OrderByDescending(x => x.CreatedDate);
                        break;

                    case "Oldest":
                        query = query.OrderBy(x => x.CreatedDate);
                        break;

                    case "PriorityDesc":
                        query = query.OrderByDescending(x => x.Priorities);
                        break;

                    case "PriorityAsc":
                        query = query.OrderBy(x => x.Priorities);
                        break;

                    default:
                        query = query.OrderByDescending(x => x.CreatedDate);
                        break;
                }
            }
            var totalItems = query.Count();

            var completed = query.Count(x => x.isCompleted);

            var pending = totalItems - completed;


            var todos = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new TodoDTO
                {
                    Id = x.Id,
                    Title = x.Name,
                    Description = x.Description,
                    Iscompleted = x.isCompleted,
                    priorities=x.Priorities,
                    CreatedDate=x.CreatedDate
                    
                })
                .ToList();


            return new PaginationDTO<TodoDTO>
            {
                Items = todos,
                CurrentPage = filter.Page,
                PageSize = filter.PageSize,
                TotalItems = totalItems,
                TotalCompleted = completed,
                TotalPending = pending
            };
        }

        public TodoItem GetById(int id ,int UserId)
        {
            return _Context.TodoItems
        .Include(c => c.User)
        .FirstOrDefault(c =>
    c.Id == id &&
    c.UserId == UserId &&
    !c.IsDeleted);
        }

        public resultOperation UpdateTodo(UpdateTodoDTO item,int UserId)
        {
            var todo = _Context.TodoItems
        .FirstOrDefault(c => c.Id == item.Id && c.UserId==UserId&& !c.IsDeleted);

            if (todo == null)
            {
                return resultOperation.Error("آیتم مورد نظر پیدا نشد مجدد تلاش کنید");
            }

            todo.Description = item.Description;
            todo.Name = item.Title;
            todo.Priorities=item.priorities;

            _Context.SaveChanges();

            return resultOperation.Success("آیتم با موفقیت بروزرسانی شد");

        }

       

        
    }
}
