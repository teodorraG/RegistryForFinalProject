﻿using System;
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
        private readonly RegistryDbContext db = new RegistryDbContext();

        public RegistryController(RegistryDbContext context)
        {
            db = context;
        }
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult BabyRegistry(RegistryViewModel babyRegistryViewModel)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        return View("BabyRegistry");
        //    }
        //    return View(babyRegistryViewModel);
        //}

        public IActionResult WeddingRegistry()
        {

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult WeddingRegistry(WeddingRegistryViewModel weddingRegistryViewModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        return View("WeddingRegistry");
        //    }
        //    return View(weddingRegistryViewModel);
        //}

        public IActionResult BirthdayRegistry()
        {

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult BirthdayRegistry(BirthdayRegistryViewModel birthdayRegistryViewModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        return View("BirthdayRegistry");
        //    }
        //    return View(birthdayRegistryViewModel);
        //}

        

        //public IActionResult ResultsWeddingRegistry()
        //{

        //    return View();
        //}

        //public IActionResult ResultsBirthdayRegistry()
        //{

        //    return View();
        //}

        

        public IActionResult RegistryRepository()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult RegistryRepository(RegistryViewModel registryViewModel, string registryType)
        {

            if (ModelState.IsValid)
            {
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                List<string> dataList = new List<string> { registryViewModel.Name, registryViewModel.City };
                if (sQLInjectionProtectionService.HasMaliciousCharacters(dataList))
                {
                    HttpContext.Session.SetString("MaliciousSymbols", Constant.MaliciousSymbols);
                    return View("Registry");
                }
                var currentUser = HttpContext.Session.GetString("CurrentUser");
                var user = db.Accounts.FirstOrDefault(x => x.UserName == currentUser);
                Registry registry = new Registry
                {
                    Name = registryViewModel.Name,
                    Location = registryViewModel.City,
                    DateOfEvent = registryViewModel.DateOfEvent,
                    AccountId = user.Id
                };
                if (registryType == Enums.RegistryType.Baby.ToString())
                {
                    registry.RegistryType = Enums.RegistryType.Baby;
                }
                else if (registryType == Enums.RegistryType.Wedding.ToString())
                {
                    registry.RegistryType = Enums.RegistryType.Wedding;
                }
                else if (registryType == Enums.RegistryType.Birthday.ToString())
                {
                    registry.RegistryType = Enums.RegistryType.Birthday;
                }
                db.Registries.Add(registry);
                db.SaveChanges();
                this.TempData["SuccessfullyCreatedRegistry"] = Constant.SuccessfullyCreatedRegistry;
                return View("Registry");
            }
            this.TempData["IncorrectRegistryForm"] = Constant.IncorrectRegistryForm;
            return View("Registry");
        }

        public IActionResult RegistryResults()
        {
            RegistryRepositoryViewModel registryRepositoryViewModel = new RegistryRepositoryViewModel { };
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

        public IActionResult FindRegistry(RegistryViewModel registryViewModel)
        {
            if (ModelState.IsValid)
            {
                var registiresResult = db.Registries.Where(x => x.Name == registryViewModel.Name && x.Location == registryViewModel.City && x.DateOfEvent == registryViewModel.DateOfEvent).ToList();
                if (registiresResult.Count() == 0)
                {
                    this.TempData["NotFoundRegistry"] = Constant.NotFoundRegistry;
                    return View("Registry");
                }
                RegistryRepositoryViewModel registryRepositoryViewModel = new RegistryRepositoryViewModel { Registries = registiresResult };
                return View("RegistryResults", registryRepositoryViewModel);
            }
            this.TempData["NotFoundRegistryForm"] = Constant.NotFoundRegistryForm;
            return View("Registry");
        }

        //[HttpPost]
        public IActionResult RedirectToRegistry(int id)
        {
            return RedirectToAction("CategoriesRegistry", "Profile", new { id = id });
        }
    }
}
