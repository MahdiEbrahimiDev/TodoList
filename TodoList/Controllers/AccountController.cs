using CoreLayer.DTOs.AccountDTO;
using CoreLayer.DTOs.UserDTO;
using CoreLayer.Services.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Models.ViewModels;

namespace TodoList.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var dto = new LoginDTO
            {
                Name = login.Name,
                Password = login.Password
            };
            var user = _userServices.userLogin(dto);

            if (user == null)
            {
                ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است.");
                return View(login);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

           var principal = new ClaimsPrincipal(identity);

await HttpContext.SignInAsync(
    CookieAuthenticationDefaults.AuthenticationScheme,
    principal,
    new AuthenticationProperties
    {
        IsPersistent = dto.RememberMe
    });

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Register(RegisterViewModel Register)
        {
            if (!ModelState.IsValid)
            {
                return View(Register);
            }
            var dto = new CreateUserDTO
            {
                Name = Register.Name,
                Password = Register.Password,
            };
            var result = _userServices.CreateUser(dto);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(Register);
            }

            TempData["Success"] = result.Message;

            return RedirectToAction("Login");
        }
    }
}