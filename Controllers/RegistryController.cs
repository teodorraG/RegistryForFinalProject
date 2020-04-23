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
using CloudinaryDotNet;
using RegistryForFinalProject.Constants;
using CloudinaryDotNet.Actions;

namespace RegistryForFinalProject.Controllers
{
    public class RegistryController : Controller
    {
        public IActionResult Registry()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(Constant.CLOUD_NAME, Constant.API_KEY, Constant.API_SECRET);
            Cloudinary cloudinary = new Cloudinary(account);
            ////var asd = cloudinary.Api.UrlImgUp.BuildImageTag("ttss28mqurpmvinr45q8.jpg");

            //var uploadParams = new ImageUploadParams()
            //{
            //    File = new FileDescription(@"C:\Users\teodo\OneDrive\Desktop\REGISTRY\registryRepo\wedd.jpg")
            //};
            //cloudinary.Upload(uploadParams);

            ////var path = uploadResult.JsonObj["public_id"];

            return View();
        } 
        public IActionResult BabyRegistry()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult BabyRegistry(RegistryViewModel babyRegistryViewModel)
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
        [ValidateAntiForgeryToken]

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
        [ValidateAntiForgeryToken]

        public IActionResult BirthdayRegistry(BirthdayRegistryViewModel birthdayRegistryViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("BirthdayRegistry");
            }
            return View(birthdayRegistryViewModel);
        }

        public IActionResult ResultsBabyRegistry()
        {

            return View();
        }

        public IActionResult ResultsWeddingRegistry()
        {

            return View();
        }

        public IActionResult ResultsBirthdayRegistry()
        {

            return View();
        }

        public IActionResult ViewRegistry()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ViewRegistry(ViewRegistryViewModel viewRegistryViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("ViewRegistry");
            }
            return View(viewRegistryViewModel);
        }

        public IActionResult RegistryRepository()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult RegistryRepository(RegistryViewModel registryViewModel)
        {

            if (ModelState.IsValid)
            {
                Registry registry = new Registry
                {
                    Name = registryViewModel.Name,
                    Location = registryViewModel.City,
                    DateOfEvent = registryViewModel.DateOfEvent,
                    RegistryType = ViewBag["Title"].ToString().Split(" ").First()
                };
            }
            return View(registryViewModel);
        }
    }
}
