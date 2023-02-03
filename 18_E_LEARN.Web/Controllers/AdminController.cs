﻿using _18_E_LEARN.BusinessLogic.Services;
using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using _18_E_LEARN.DataAccess.Validation.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _18_E_LEARN.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserService _userService;

        public object AddModelState { get; private set; }

        public AdminController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserVM model)
        {
            var valdator = new LoginUserValidation();
            var validationresult = await valdator.ValidateAsync(model);
            if (validationresult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Admin");
                }
                // write code
                ViewBag.AuthError = result.Message;
                return View(model);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}