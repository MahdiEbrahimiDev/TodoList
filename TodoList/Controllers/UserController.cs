using CoreLayer.DTOs.UserDTO;
using CoreLayer.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Models.ViewModels;

namespace TodoList.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        
        private readonly IUserServices userServices;
        public UserController(IUserServices user)
        {
            userServices= user;
        }


        
        public IActionResult Profile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var model = userServices.GetProfile(userId);
            var user = new ProfileViewModel
            {
                CompletedTasks = model.CompletedTasks,
                CreatedTime = model.CreatedTime,
                Name = model.Name,
                PendingTasks = model.PendingTasks,
                TotalTasks = model.TotalTasks
            };

            if (model == null)
            {
                return NotFound();
            }

            return View(user);
        }




        // نمایش صفحه ساخت کاربر
       // [HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}



        //// ساخت کاربر
        //[HttpPost]
        //public IActionResult Create(CreateUserDTO dto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(dto);
        //    }


        //    var result = userServices.CreateUser(dto);


        //    if (!result.IsSuccess)
        //    {
        //        ModelState.AddModelError("", result.Message);
        //        return View(dto);
        //    }


        //    TempData["Success"] = result.Message;

        //    return RedirectToAction("profile");
        //}




        // نمایش صفحه ویرایش
        [HttpGet]
        
        public IActionResult Edit()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var user = userServices.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateUserDTO
            {
                Id = user.Id,
                Name = user.Name
            };

            return View(model);
        }





        // ویرایش کاربر
        [HttpPost]
        public IActionResult Edit(UpdateUserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }


            dto.Id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = userServices.UpdateUser(dto);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);

                return View(dto);
            }


            TempData["Success"] = result.Message;


            return RedirectToAction("Profile");
        }




        // حذف کاربر
        //public IActionResult Delete(int id)
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        //    var result = userServices.DeleteUser(userId);


        //    if (result.IsSuccess)
        //    {
        //        TempData["Success"] = result.Message;
        //    }
        //    else
        //    {
        //        TempData["Error"] = result.Message;
        //    }


        //    return RedirectToAction("profile");
        //}

    }



}
