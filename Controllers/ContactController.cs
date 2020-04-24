using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Services;
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
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                List<string> dataList = new List<string> { contactViewModel.Name, contactViewModel.Email, contactViewModel.Subject, contactViewModel.Message };
                if (sQLInjectionProtectionService.HasMaliciousCharacters(dataList))
                {
                    ViewData["MaliciousSymbols"] = Constant.MaliciousSymbols;
                    return View();
                }

                this.TempData["SuccessfullySentEmail"] = Constant.SuccessfullySentEmail;
                return View("Contact");
            }

            return View(contactViewModel);
        }
    }
}
