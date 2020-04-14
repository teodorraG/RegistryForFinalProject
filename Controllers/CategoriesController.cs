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
            var items = db.Items.ToList();
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
                newCategoriesViewModel.Items = db.Items.ToList();
                return View(newCategoriesViewModel);
            }

            var categoryId = db.Categories.FirstOrDefault(x => x.Name == categoryName).Id;
            var allItemsToDisplay = db.Items.Where(x => x.CategoryId == categoryId).ToList();
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

            var searchedItems = db.Items.Where(x => x.Title.ToLower().Contains(categoriesViewModel.Search.ToLower())).ToList();

            CategoriesViewModel newCategoriesViewModel = new CategoriesViewModel();

            newCategoriesViewModel.Items = searchedItems;
            foreach (var item in db.Categories)
            {
                newCategoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
            return View("Categories", newCategoriesViewModel);
        }
    }
}
