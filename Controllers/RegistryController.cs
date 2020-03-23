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

namespace RegistryForFinalProject.Controllers
{
    public class RegistryController : Controller
    {
        public IActionResult Registry()
        {
            return View();
        } 
        public IActionResult BabyRegistry()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult BabyRegistry(BabyRegistryViewModel babyRegistryViewModel)
        {
            
            if (ModelState.IsValid)
            {
                return View("BabyRegistry");
            }
            return View(babyRegistryViewModel);
        }

        public IActionResult WeddingRegistry()
        {

            return View();
        }
        [HttpPost]
        public IActionResult WeddingRegistry(WeddingRegistryViewModel weddingRegistryViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("WeddingRegistry");
            }
            return View(weddingRegistryViewModel);
        }

        public IActionResult BirthdayRegistry()
        {

            return View();
        }
        [HttpPost]
        public IActionResult BirthdayRegistry(BirthdayRegistryViewModel birthdayRegistryViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("BirthdayRegistry");
            }
            return View(birthdayRegistryViewModel);
        }
    }
}
