using CoreLayer.DTOs.TodoDTO;
using CoreLayer.Services.TodoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoServices todoServices;
        public TodoController(ITodoServices todo)
        {
            todoServices = todo;
        }
        public IActionResult Index(TodoFilterDTO filter)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);


            int pageSize = 5;


            var result = todoServices.GetAllTodo(
                userId,
               filter);


            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTodoDTO DTO)
        {
           
            if (!ModelState.IsValid)
            {
                return View(DTO);
            }
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = todoServices.CreateTodo(DTO, userId);

           

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(DTO);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = todoServices.GetById(id, userId);

            if (result == null)
            {
                return NotFound();
            }


            var model = new UpdateTodoDTO
            {
                Id = result.Id,
                Title = result.Name,
                Description = result.Description,
                priorities = result.Priorities
            };


            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateTodoDTO DTO)
        {
            if (ModelState.IsValid)
            {
                var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var upadate = todoServices.UpdateTodo(DTO,userid);
                if (upadate.IsSuccess)
                {
                  return  RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", upadate.Message);
                    return View(DTO);
                }
            }
            return View(DTO);

        }
        public IActionResult Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = todoServices.DeleteTodo(id, userId);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = result.Message;
            // ModelState.AddModelError("",result.Message);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeStatus(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = todoServices.CompletedTodo(id, userId);
            if (result.IsSuccess)
            {
                TempData["Success"] = result.Message;
                
            }
             else 
            {
                
                TempData["Error"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
