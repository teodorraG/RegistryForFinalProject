﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RegistryForFinalProject.Constants;

namespace RegistryForFinalProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly RegistryDbContext db = new RegistryDbContext();
        public ProfileController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
            ProfileViewModel profileViewModel = new ProfileViewModel { Username = account.UserName, Address = account.Address, Gender = account.Gender, FirstName = account.FirstName, LastName = account.LastName };
            return View(profileViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UpdateAccountInfo(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("CurrentUser");
                var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);

                account.FirstName = profileViewModel.FirstName;
                account.LastName = profileViewModel.LastName;
                account.Address = profileViewModel.Address;

                if (profileViewModel.Gender != 0)
                {
                    account.Gender = profileViewModel.Gender;
                }
                else if (profileViewModel.Gender == 0)
                {
                    this.TempData["InvalidGender"] = Constant.InvalidGender;
                    return RedirectToAction("Profile");
                }
                db.SaveChanges();
                this.TempData["MadeChanges"] = Constant.MadeChanges;
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UpdatePassword(ProfileViewModel profileViewModel)
        {

            if (ModelState.IsValid)
            {
                if (profileViewModel.CurrentPassword == null || profileViewModel.NewPassword == null)
                {
                    this.TempData["NoDataEntered"] = Constant.NoDataEntered;
                    return RedirectToAction("Profile");
                }
                else if (profileViewModel.CurrentPassword != null && profileViewModel.NewPassword != null)
                {
                    var userName = HttpContext.Session.GetString("CurrentUser");
                    var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
                    var password = PasswordEncodingService.GetHashSha256(profileViewModel.CurrentPassword);

                    var newPassword = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);


                    if (password == newPassword)
                    {
                        this.TempData["MatchingPassword"] = Constant.MatchingPassword;
                        return RedirectToAction("Profile");
                    }
                    else if (password == account.Password)
                    {
                        account.Password = PasswordEncodingService.GetHashSha256(profileViewModel.NewPassword);
                        db.SaveChanges();
                        this.TempData["MadeChangesToPass"] = Constant.MadeChangesToPass;
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        this.TempData["ErrorChanges"] = Constant.ErrorChanges;
                        return RedirectToAction("Profile");
                    }
                }
            }
            //return LocalRedirect("~/Account/LogIn");
            this.TempData["ErrorComplexity"] = Constant.ErrorComplexity;
            return RedirectToAction("Profile");
        }

        public IActionResult Orders()
        {
            var currentUserName = HttpContext.Session.GetString("CurrentUser");
            var ordererId = db.Accounts.FirstOrDefault(x => x.UserName == currentUserName).Id;
            var currentOrderedItems = db.Orders.Where(x => x.BuyerId == ordererId).ToList();
            var items = new List<Item>();

            foreach (var item in currentOrderedItems)
            {
                var currentItem = db.Items.FirstOrDefault(x => x.Id == item.ItemId);
                currentItem.Quantity = item.Quantity;
                items.Add(currentItem);
            }

            OrdersViewModel ordersViewModel = new OrdersViewModel { Items = items };
            return View(ordersViewModel);
        }

        public IActionResult Offers()
        {
            var currentUserName = HttpContext.Session.GetString("CurrentUser");
            var sellerId = db.Accounts.FirstOrDefault(x => x.UserName == currentUserName).Id;
            var currentSelledItems = db.Items.Where(x => x.SellerId == sellerId).ToList();
            var items = new List<Item>();

            foreach (var item in currentSelledItems)
            {
                items.Add(item);
            }

            OffersViewModel offersViewModel = new OffersViewModel { Items = items };
            return View(offersViewModel);
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
            var item = db.Items.FirstOrDefault(x => x.Id == id);

            var itemToRemoveFromDb = db.Items.FirstOrDefault(x => x.Id == item.Id && x.SellerId == account.Id);
            db.Items.Remove(itemToRemoveFromDb);
            db.SaveChanges();
            this.TempData["PermanentlyDeletedItem"] = Constant.PermanentlyDeletedItem;
            return RedirectToAction("Offers");
        }

        public IActionResult EditItem()
        {
            int id = int.Parse(HttpContext.Session.GetString("ItemId"));
            var item = db.Items.FirstOrDefault(x => x.Id == id);


            ItemViewModel itemViewModel = new ItemViewModel
            {
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                Image1 = item.Image1,
                Image2 = item.Image2,
                Image3 = item.Image3,
                SelectedCategory = db.Categories.FirstOrDefault(x => x.Id == item.CategoryId).Name
            };
            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditItem(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(HttpContext.Session.GetString("ItemId"));
                var item = db.Items.FirstOrDefault(x => x.Id == id);
                Category category = db.Categories.FirstOrDefault(x => x.Name == itemViewModel.SelectedCategory);
                item.Title = itemViewModel.Title;
                item.Price = itemViewModel.Price;
                item.Quantity = itemViewModel.Quantity;
                item.Category = category;
                item.Description = itemViewModel.Description;

                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(Constant.CLOUD_NAME, Constant.API_KEY, Constant.API_SECRET);
                Cloudinary cloudinary = new Cloudinary(account);
                if (itemViewModel.Image1 != string.Empty && itemViewModel.Image1 != null)
                {

                    if (itemViewModel.Image1.Length > 100)
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(itemViewModel.Image1)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        var path = uploadResult.JsonObj["public_id"].ToString();
                        item.Image1 = path;
                    }
                }
                else
                {
                    item.Image1 = Constants.Constant.NO_IMAGE;
                }

                if (itemViewModel.Image2 != string.Empty && itemViewModel.Image2 != null)
                {

                    if (itemViewModel.Image2.Length > 100)
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(itemViewModel.Image2)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        var path = uploadResult.JsonObj["public_id"].ToString();
                        item.Image2 = path;
                    }
                }
                else
                {
                    item.Image2 = Constants.Constant.NO_IMAGE;
                }


                if (itemViewModel.Image3 != string.Empty && itemViewModel.Image3 != null)
                {

                    if (itemViewModel.Image3.Length > 100)
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(itemViewModel.Image3)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        var path = uploadResult.JsonObj["public_id"].ToString();
                        item.Image3 = path;
                    }
                }
                else
                {
                    item.Image3 = Constants.Constant.NO_IMAGE;
                }
                db.SaveChanges();
                this.TempData["SuccessfullyEdited"] = Constant.SuccessfullyEdited;
                return RedirectToAction("Offers");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RedirectToEditItem(string id)
        {
            
            HttpContext.Session.SetString("ItemId", id);
            return RedirectToAction("EditItem", "Profile");
        }

        public IActionResult Shipping()
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
            var userItems = db.Items.Where(x => x.SellerId == account.Id).ToList();

            ShippingViewModel shippingViewModel = new ShippingViewModel();

            foreach (var order in db.Orders.Where(x => x.SellerId == account.Id && x.ShippingStatus != Enums.ShippingStatus.Done).ToList())
            {
                shippingViewModel.Orders.Add(order);
                shippingViewModel.Items.Add(db.Items.FirstOrDefault(x => x.Id == order.ItemId));

            }

            return View(shippingViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Shipping(ShippingViewModel shippingViewModel, string id)
        {
            var currentOrder = db.Orders.FirstOrDefault(x => x.Id == shippingViewModel.Orders[int.Parse(id)].Id);
            if (shippingViewModel.Orders[int.Parse(id)].ShippingStatus == Enums.ShippingStatus.Cancelled)
            {
                currentOrder.ShippingStatus = Enums.ShippingStatus.Cancelled;

            }
            else if (shippingViewModel.Orders[int.Parse(id)].ShippingStatus == Enums.ShippingStatus.Delivered)
            {
                currentOrder.ShippingStatus = Enums.ShippingStatus.Delivered;

            }
            else if (shippingViewModel.Orders[int.Parse(id)].ShippingStatus == Enums.ShippingStatus.Done)
            {
                currentOrder.ShippingStatus = Enums.ShippingStatus.Done;

            }
            else if (shippingViewModel.Orders[int.Parse(id)].ShippingStatus == Enums.ShippingStatus.Processing)
            {
                currentOrder.ShippingStatus = Enums.ShippingStatus.Processing;

            }
            else if (shippingViewModel.Orders[int.Parse(id)].ShippingStatus == Enums.ShippingStatus.Shipped)
            {
                currentOrder.ShippingStatus = Enums.ShippingStatus.Shipped;
            }
            db.SaveChanges();
            this.TempData["SuccessfullyChangedStatus"] = Constant.SuccessfullyChangedStatus;
            return RedirectToAction("Shipping");
        }

        public IActionResult RegistryRepository()
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName).Id;

            var userRegistries = db.Registries.Where(x => x.AccountId == account).ToList();

            var registries = new List<Registry>();

            foreach (var reg in userRegistries)
            {
                registries.Add(reg);
            }

            RegistryRepositoryViewModel registryRepositoryViewModel = new RegistryRepositoryViewModel { Registries = registries };
            return View("RegistryRepository", registryRepositoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegistryRepository(CategoriesRegistryViewModel categoriesRegistryViewModel, int id)
        {

            var registryItems = db.RegistryItems.Where(x => x.RegistryId == id).ToList();

            List<Item> items = new List<Item>();

            foreach (var item in registryItems)
            {
                var currentItem = db.Items.FirstOrDefault(x => x.Id == item.ItemId);
                if (currentItem.Description.Length >= 132)
                {
                    item.Item.Description = item.Item.Description.Substring(0, 123);
                    item.Item.Description += " . . ";
                }
                items.Add(currentItem);

            }


            categoriesRegistryViewModel = new CategoriesRegistryViewModel
            {
                Items = items
            };
            return View("CategoriesRegistry", categoriesRegistryViewModel);

        }

    }
}
