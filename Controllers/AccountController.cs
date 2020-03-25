using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Enums;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Services;

namespace RegistryForFinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly RegistryContext db = new RegistryContext();
        public AccountController(RegistryContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel logViewModel)
        {
            if (ModelState.IsValid)
            {
                var password = PasswordEncodingService.GetHashSha256(logViewModel.Password);
                var user = db.Accounts.FirstOrDefault(x => x.UserName == logViewModel.UserName && x.Password == password);
                if (user != null)
                {
                    HttpContext.Session.SetString("CurrentUser", user.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("LogIn");
                }
            }
            return View(logViewModel);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountViewModel accViewModel)
        {
            if (ModelState.IsValid)
            {
                
                int termsCheckBox = Request.Form["TermsCheckBox"].Count;
                int ageCheckBox = Request.Form["AgeCheckBox"].Count;
                if (termsCheckBox == 1 && ageCheckBox == 1)
                {
                    Account account = new Account
                    {
                        UserName = accViewModel.UserName,
                        Password = PasswordEncodingService.GetHashSha256(accViewModel.Password),
                        Email = accViewModel.Email,
                        Role = Role.User
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return View("SuccessfullyRegistered");
                }
                else
                {
                    ViewData["LoginError"] = "You must agree to the terms and be above 18 years old.";
                    return View();
                }

            }
            return View();

        }
        public IActionResult ForgottenPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgottenPassword(ForgottenPasswordViewModel forgottenPassViewModel)
        {
            if (ModelState.IsValid)
            {
                return View("ForgottenPassword");
            }
            return View(forgottenPassViewModel);
        }
        public IActionResult SuccessfullyRegistered()
        {
            return View("SuccessfullyRegistered");
        }
    }
}