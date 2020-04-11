using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RegistryForFinalProject.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Contact(ContactUsViewModel contactViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("Contact");
            }

            return View(contactViewModel);
        }
    }
}
