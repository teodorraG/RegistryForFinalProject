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
            //    File = new FileDescription(@"C:\Users\teodo\OneDrive\Desktop\REGISTRY\carousel06.jpg")
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
