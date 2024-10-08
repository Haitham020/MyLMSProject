﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLMSProject.Models;
using MyLMSProject.Models.ViewModels;

namespace MyLMSProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
      
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existedEmail = await _userManager.FindByEmailAsync(model.Email!);
                if(existedEmail != null)
                {
                    ModelState.AddModelError("Mobile", "Password or Email credentials wrong");
                    return View(model);
                }

                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.Mobile,
                    Active = false,
                    InActive = true,
                };
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email!);
                if (user != null)
                {
                    if (user.InActive)
                    {
                        ModelState.AddModelError("", "Your account is still pending approval. Please wait.");
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password!, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error Logging In, Please Try Again.");
                    }
                    return View(model);
                }
                    
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
