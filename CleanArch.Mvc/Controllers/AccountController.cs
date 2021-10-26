using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
              
            CheckUser checkUser = _userService.CheckUser(register.UserName, register.Email);

            if (checkUser != CheckUser.Ok)
            {
                ViewBag.Check = checkUser;
                return View(register);
            }

            var user = new User
            {
                Email = register.Email,
                UserName = register.UserName,
                Password = PasswordHelper.EncodePasswordMd5(register.Password)
            };
            _userService.RegisterUser(user);

           return View("SuccessRegister",register);
        }
    }
}
