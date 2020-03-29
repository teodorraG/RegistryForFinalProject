﻿using System;
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
using RegistryForFinalProject.Constants;

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
                if (db.Accounts.FirstOrDefault(x=>x.UserName == logViewModel.UserName && x.Password == password) != null)
                {
                    var user = db.Accounts.FirstOrDefault(x => x.UserName == logViewModel.UserName && x.Password == password);
                    HttpContext.Session.SetString("CurrentUser", user.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["InvalidUser"] = Constant.LogInInvalidUserCredentialsError;
                    return View();
                }
                
            }
            return View();
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
                if (db.Accounts.FirstOrDefault(x=>x.UserName == accViewModel.UserName) != null)
                {
                    ViewData["UsernameError"] = Constant.UsernameAlreadyExists;
                }
                if (db.Accounts.FirstOrDefault(x=>x.Email == accViewModel.Email) != null)
                {
                    ViewData["EmailError"] = Constant.EmailAlreadyExists;
                }
                if (ViewData["UsernameError"] != null || ViewData["EmailError"] != null)
                {
                    return View();
                }
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
                    ViewData["LoginError"] = Constant.LogInError;
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