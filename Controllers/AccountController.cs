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
            //var currentUserName = HttpContext.Session.GetString("CurrentUser");
            //var registeredAccount = db.Accounts.FirstOrDefault(x => x.UserName == currentUserName);

            //if (registeredAccount == null)
            //{
            //    return RedirectToAction("NotFoundError", "Error");

            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

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
        [ValidateAntiForgeryToken]

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
                    //??
                    //&&
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
                    //??
                    // termsCheckBox = 0;
                    // ageCheckBox =0;
                    //return View("SuccessfullyRegistered");
                    this.TempData["SuccessfullyRegistered"] = "Successfully registered. Please Sign In to continue!";
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
                if (db.Accounts.FirstOrDefault(x => x.Email == forgottenPassViewModel.Email) != null)
                {
                    this.TempData["SentEmail"] = "An email has been sent to you.";
                    return RedirectToAction("ForgottenPassword");
                }
            }
            return View();
        }
    }
}