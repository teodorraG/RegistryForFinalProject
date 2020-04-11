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
        private readonly RegistryDbContext db = new RegistryDbContext();
        public ProfileController(RegistryDbContext registryContext)
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
        [ValidateAntiForgeryToken]

        public IActionResult UpdateAccountInfo(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("CurrentUser");
                var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);

                account.FirstName = profileViewModel.FirstName;
                account.LastName = profileViewModel.LastName;
                account.Address = profileViewModel.Address;

                if (profileViewModel.Gender != 0)
                {
                    account.Gender = profileViewModel.Gender;
                }
                else if (profileViewModel.Gender == 0)
                {
                    this.TempData["InvalidGender"] = "Select your gender type";
                    return RedirectToAction("Profile");
                }
                db.SaveChanges();
                this.TempData["MadeChanges"] = "Your changes have been saved";
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UpdatePassword(ProfileViewModel profileViewModel)
        {

            if (ModelState.IsValid)
            {
                if (profileViewModel.CurrentPassword == null || profileViewModel.NewPassword == null)
                {
                    this.TempData["NoDataEntered"] = "Enter valid data";
                    return RedirectToAction("Profile");
                }
                else if (profileViewModel.CurrentPassword != null && profileViewModel.NewPassword != null)
                {
                    var userName = HttpContext.Session.GetString("CurrentUser");
                    var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
                    var password = PasswordEncodingService.GetHashSha256(profileViewModel.CurrentPassword);

                    var newPassword = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);


                    if (password == newPassword)
                    {
                        this.TempData["MatchingPassword"] = "The new password matches current password";
                        return RedirectToAction("Profile");
                    }
                    else if (password == account.Password)
                    {
                        account.Password = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);
                        db.SaveChanges();
                        this.TempData["MadeChanges"] = "Your changes have been saved";
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        this.TempData["ErrorChanges"] = "Make sure your password is valid";
                        return RedirectToAction("Profile");
                    }
                }
            }
            //return LocalRedirect("~/Account/LogIn");
            this.TempData["ErrorComplexity"] = "Password must be 8 or more characters, to contain at least one: lower case letter, upper case letter and number";
            return RedirectToAction("Profile");
        }

        public IActionResult Orders()
        {

            return View("Orders");
        }

        public IActionResult Offers()
        {

            return View("Offers");
        }
    }
}
