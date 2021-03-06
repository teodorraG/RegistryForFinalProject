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
        private readonly RegistryDbContext db = new RegistryDbContext();
        public AccountController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult LogIn(LogInViewModel logViewModel)
        {
            if (ModelState.IsValid)
            {
                var password = PasswordEncodingService.GetHashSha256(logViewModel.Password);
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                if (sQLInjectionProtectionService.HasMaliciousCharacters(logViewModel.UserName))
                {
                    ViewData["MaliciousSymbols"] = Constant.MaliciousSymbols;
                    return View();
                }
                if (db.Accounts.FirstOrDefault(x=>x.UserName == logViewModel.UserName && x.Password == password) != null)
                {
                    var user = db.Accounts.FirstOrDefault(x => x.UserName == logViewModel.UserName && x.Password == password);
                    HttpContext.Session.SetString("CurrentUser", user.UserName);
                    HttpContext.Session.SetString("CurrentUserId", user.Id.ToString());
                    if (user.Role == Role.Admin)
                    {
                        HttpContext.Session.SetString("CurrentUserIsAdmin", "true");
                    }
                    else
                    {
                        HttpContext.Session.SetString("CurrentUserIsAdmin", "false");
                    }
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
            HttpContext.Session.Remove("CurrentUserId");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Register(AccountViewModel accViewModel)
        {
            if (ModelState.IsValid)
            {
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                List<string> dataList = new List<string> { accViewModel.UserName, accViewModel.Email, accViewModel.Password, accViewModel.ConfirmPassword };
                if (sQLInjectionProtectionService.HasMaliciousCharacters(dataList))
                {
                    ViewData["MaliciousSymbols"] = Constant.MaliciousSymbols;
                    return View();
                }

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

                    this.TempData["SuccessfullyRegistered"] = Constant.SuccessfullyRegistered;
                    return View();
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
        [ValidateAntiForgeryToken]

        public IActionResult ForgottenPassword(ForgottenPasswordViewModel forgottenPassViewModel)
        {
            if (ModelState.IsValid)
            {
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                if (sQLInjectionProtectionService.HasMaliciousCharacters(forgottenPassViewModel.Email))
                {
                    ViewData["MaliciousSymbols"] = Constant.MaliciousSymbols;
                    return View();
                }
                if (db.Accounts.FirstOrDefault(x => x.Email == forgottenPassViewModel.Email) != null)
                {
                    this.TempData["SentEmail"] = Constant.SentEmail;
                    return RedirectToAction("ForgottenPassword");
                }
            }
            this.TempData["NotMatchingEmail"] = Constant.NotMatchingEmail;
            return View();
        }
    }
}