﻿using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                this.TempData["SuccessfullySentEmail"] = "Your message was sent successfully";
                return View("Contact");
            }

            return View(contactViewModel);
        }
    }
}
