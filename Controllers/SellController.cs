﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class SellController : Controller
    {
        private readonly RegistryDbContext db = new RegistryDbContext();


        public SellController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult Sell()
        {
            ItemViewModel sellViewModel = new ItemViewModel();
            return View(sellViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Sell(ItemViewModel sellViewModel)
        {
          
            var seller = db.Accounts.FirstOrDefault(x => x.UserName == HttpContext.Session.GetString("CurrentUser"));
            Category category = db.Categories.FirstOrDefault(x => x.Name == sellViewModel.SelectedCategory);
            Item item = new Item { Title = sellViewModel.Title, Price = sellViewModel.Price, Quantity = sellViewModel.Quantity, Category = category, Description = sellViewModel.Description, Seller = seller };

            if (ModelState.IsValid)
            {
                SQLInjectionProtectionService sQLInjectionProtectionService = new SQLInjectionProtectionService();
                List<string> dataList = new List<string> { sellViewModel.Title, sellViewModel.Description };
                if (sQLInjectionProtectionService.HasMaliciousCharacters(dataList))
                {
                    HttpContext.Session.SetString("MaliciousSymbols", Constant.MaliciousSymbols);
                    return RedirectToAction("Sell");
                }

                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(Constant.CLOUD_NAME, Constant.API_KEY, Constant.API_SECRET);
                Cloudinary cloudinary = new Cloudinary(account);
                if (sellViewModel.Image1 != null && sellViewModel.Image1 != string.Empty)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(sellViewModel.Image1)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    var path = uploadResult.JsonObj["public_id"].ToString();
                    item.Image1 = path;

                }
                else
                {
                    item.Image1 = Constants.Constant.NO_IMAGE;
                }

                if (sellViewModel.Image2 != null && sellViewModel.Image2 != string.Empty)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(sellViewModel.Image2)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    var path = uploadResult.JsonObj["public_id"].ToString();
                    item.Image2 = path;
                }
                else
                {
                    item.Image2 = Constants.Constant.NO_IMAGE;
                }

                if (sellViewModel.Image3 != null && sellViewModel.Image3 != string.Empty)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(sellViewModel.Image3)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    var path = uploadResult.JsonObj["public_id"].ToString();
                    item.Image3 = path;
                }
                else
                {
                    item.Image3 = Constants.Constant.NO_IMAGE;
                }
                db.Items.Add(item);
                db.SaveChanges();
                this.TempData["SuccessfullyListed"] = Constant.SuccessfullyListed;
                return RedirectToAction("Sell");
            }
            if (item.CategoryId == 0)
            {
                this.TempData["NoCategorySelected"] = Constant.NoCategorySelected;
                return View(new ItemViewModel());
            }
            return View(new ItemViewModel());
        }
    }
}
