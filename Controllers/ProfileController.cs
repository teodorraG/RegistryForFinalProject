using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Services;

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
            return View();
        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                //if (db.Accounts.FirstOrDefault(x => x.FirstName != null || x.LastName != null || x.Address != null || x.Gender != null))
                //{

                //    Account account = db.Accounts.FirstOrDefault(x => x.UserName == profileViewModel.Username);
                //    {
                //        account.FirstName = profileViewModel.FirstName,
                //    account.LastName = profileViewModel.LastName,
                //    account.Address = profileViewModel.Address,
                //    if (profileViewModel.Gender == 1)
                //        {
                //            account.Gender = Gender.Male,
                //        }
                //        else
                //        {
                //            account.Gender = Gender.Female,
                //        }

                //    };
                //    db.Accounts.Update(account);
                //    db.SaveChanges();
                //    this.TempData["MadeChanges"] = "Your changes have been saved";
                //}
            }
            if (ModelState.IsValid)
            {
                var password = PasswordEncodingService.GetHashSha256(profileViewModel.CurrentPassword);
                if (db.Accounts.FirstOrDefault(x => x.Password == password) != null)
                {
                    Account account = db.Accounts.FirstOrDefault(x => x.UserName == profileViewModel.Username);
                    {
                        account.Password = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);
                    }
                    db.Accounts.Update(account);
                    db.SaveChanges();
                    this.TempData["MadeChanges"] = "Your changes have been saved";
                    return View("Profile");
                }
                return View(profileViewModel);
            }
            return View();
        }
    }
}
