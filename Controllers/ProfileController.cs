using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Services;
using Microsoft.AspNetCore.Http;


namespace RegistryForFinalProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly RegistryContext db = new RegistryContext();
        public ProfileController(RegistryContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
            ProfileViewModel profileViewModel = new ProfileViewModel { Username = account.UserName, Address = account.Address, Gender = account.Gender, FirstName = account.FirstName, LastName = account.LastName };
            return View(profileViewModel);
        }
        [HttpPost]
        public IActionResult UpdateAccountInfo(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("CurrentUser");
                var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
                if (profileViewModel.FirstName != null)
                {
                    account.FirstName = profileViewModel.FirstName;
                }
                if (profileViewModel.LastName != null)
                {
                    account.LastName = profileViewModel.LastName;
                } 
                if (profileViewModel.Address != null)
                {
                    account.Address = profileViewModel.Address;
                }
                if (profileViewModel.Gender != 0)
                {
                    account.Gender = profileViewModel.Gender;
                }
                db.SaveChanges();
                this.TempData["MadeChanges"] = "Your changes have been saved";
            }
            return RedirectToAction("Profile");
        }
        [HttpPost]

        public IActionResult UpdatePassword(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("CurrentUser");
                var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
                var password = PasswordEncodingService.GetHashSha256(profileViewModel.CurrentPassword);

                if (password == account.Password)
                {
                    account.Password = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);
                    db.SaveChanges();
                    this.TempData["MadeChanges"] = "Your changes have been saved";
                }
                else
                {
                    this.TempData["ErrorChanges"] = "Make sure your password is valid";
                }
            }
            return View("Profile");
        }
    }
}
