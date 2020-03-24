using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;

namespace RegistryForFinalProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Profile(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                return View("Profile");
            }
            return View(profileViewModel);
        }
    }
}
