using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistryForFinalProject.Models;
using Microsoft.AspNetCore.Http;

namespace RegistryForFinalProject.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly RegistryDbContext db = new RegistryDbContext();

        public CategoriesController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult Categories()
        {
            var categories = db.Categories.ToList();
            var items = db.Items.Where(x=>x.Quantity > 0).ToList();
            CategoriesViewModel categoriesViewModel = new CategoriesViewModel();
            foreach (var item in db.Categories)
            {
                categoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
            categoriesViewModel.Items = items;

            foreach (var item in categoriesViewModel.Items)
            {
                if (item.Description.Length >= 132)
                {
                    item.Description = item.Description.Substring(0, 123);
                    item.Description += " . . ";
                }

            }
            return View(categoriesViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Categories(CategoriesViewModel categoriesViewModel)
        {
            CategoriesViewModel newCategoriesViewModel = new CategoriesViewModel();

            var categoryName = categoriesViewModel.SelectedCategory;

            foreach (var item in db.Categories)
            {
                newCategoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
            if (categoryName == "All Categories")
            {
                newCategoriesViewModel.Items = db.Items.Where(x=>x.Price <= categoriesViewModel.Price).ToList();
                foreach (var item in newCategoriesViewModel.Items)
                {
                    if (item.Description.Length >= 132)
                    {
                        item.Description = item.Description.Substring(0, 123);
                        item.Description += " . . ";
                    }

                }
                return View(newCategoriesViewModel);
            }

            var categoryId = db.Categories.FirstOrDefault(x => x.Name == categoryName).Id;
            var allItemsToDisplay = db.Items.Where(x => x.CategoryId == categoryId && x.Price <= categoriesViewModel.Price).ToList();
            var categories = db.Categories.ToList();

            
            newCategoriesViewModel.Items = allItemsToDisplay;

            foreach (var item in allItemsToDisplay)
            {
                if (item.Description.Length >= 132)
                {
                    item.Description = item.Description.Substring(0, 123);
                    item.Description += " . . ";
                }

            }
            return View(newCategoriesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Search(CategoriesViewModel categoriesViewModel)
        {
            var searchResult = categoriesViewModel.Search;
            if (searchResult == null)
            {
                this.TempData["Search"] = "No entered content";
                return RedirectToAction("Categories");
            }

            var searchedItems = db.Items.Where(x => x.Title.ToLower().Contains(categoriesViewModel.Search.ToLower())).ToList();

            CategoriesViewModel newCategoriesViewModel = new CategoriesViewModel();

            newCategoriesViewModel.Items = searchedItems;

            foreach (var item in db.Categories)
            {
                newCategoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }

            foreach (var item in newCategoriesViewModel.Items)
            {
                if (item.Description.Length >= 132)
                {
                    item.Description = item.Description.Substring(0, 123);
                    item.Description += " . . ";
                }

            }

            return View("Categories", newCategoriesViewModel);
        }

        public IActionResult PreviewItem(string id)
        {
            var item = db.Items.FirstOrDefault(x => x.Id == int.Parse(id));
            PreviewItemViewModel previewItemViewModel = new PreviewItemViewModel { Item = item };
            HttpContext.Session.SetString("CurrentItem", item.Id.ToString());
            return View(previewItemViewModel);
        }

        [HttpPost]
        public IActionResult PreviewItem(PreviewItemViewModel previewItemViewModel)
        {
            string userName = HttpContext.Session.GetString("CurrentUser");
            var currentUserId = db.Accounts.FirstOrDefault(x => x.UserName == userName).Id;

            int itemId = int.Parse(HttpContext.Session.GetString("CurrentItem"));
            var alreadyInCart = db.ShoppingCarts.FirstOrDefault(x => x.ItemId == itemId && x.AccountId == currentUserId && x.IsPurchased == false);
            if (alreadyInCart != null)
            {
                //if (alreadyInCart != null && alreadyInCart.IsPurchased == false && currentUserId == alreadyInCart.AccountId)
                //{
                    this.TempData["AlreadyInCart"] = "This item is already in your shopping cart";
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                //}
            }

            

            string username = HttpContext.Session.GetString("CurrentUser");
            var user = db.Accounts.FirstOrDefault(x => x.UserName == username);
            db.ShoppingCarts.Add(new ShoppingCart { ItemId = itemId, AccountId = user.Id });
            db.SaveChanges();
            return RedirectToAction("ShoppingCart", "ShoppingCart");
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            List<Item> productItems = db.Items.ToList();
            var pager = new Pager(productItems.Count(), page);

            var viewModel = new CategoriesViewModel
            {
                Items = productItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                Pager = pager
            };

            return View(viewModel);
        }

        
    }
}
