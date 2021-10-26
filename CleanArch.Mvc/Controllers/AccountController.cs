using CleanArch.Application.Interfaces;
using CleanArch.Application.Security;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace CleanArch.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        #region Register
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

            return View("SuccessRegister", register);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(string ReturnUrl = "/")
        {
            ViewBag.Return = ReturnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            if (!_userService.IsUserExist(login.Email, login.Password))
            {
                ModelState.AddModelError("Email", "User Not Found...");
                return View();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,login.Email),
                new Claim(ClaimTypes.NameIdentifier,login.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);

            return Redirect(ReturnUrl);
        }


        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/"); 
        }
            #endregion
        }
}
