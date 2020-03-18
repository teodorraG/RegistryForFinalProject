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
using System.Text;
using System.Security.Cryptography;

namespace RegistryForFinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly RegistryContext db = new RegistryContext();
        public AccountController()
        {
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
                return View("LogIn");
            }
            return View(logViewModel);
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
                        Password = GetHashSha256(accViewModel.Password),
                        Email = accViewModel.Email,
                        Role = Role.User
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return View("LogIn");
                }
                else
                {
                    return View("Index");
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

        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}