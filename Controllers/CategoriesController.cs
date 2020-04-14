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
            var categoryName = categoriesViewModel.SelectedCategory;
            var categoryId = db.Categories.FirstOrDefault(x => x.Name == categoryName).Id;
            var allItemsToDisplay = db.Items.Where(x => x.CategoryId == categoryId).ToList();
            var categories = db.Categories.ToList();
            CategoriesViewModel newCategoriesViewModel = new CategoriesViewModel();

            foreach (var item in db.Categories)
            {
                newCategoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
            }
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Filter(CategoriesViewModel categoriesViewModel)
        //{
        //    var categoryName = categoriesViewModel.SelectedCategory;
        //    var categoryId = db.Categories.FirstOrDefault(x => x.Name == categoryName).Id;
        //    var allItemsToDisplay = db.Items.Where(x => x.CategoryId == categoryId).ToList();
        //    var categories = db.Categories.ToList();
        //    CategoriesViewModel newCategoriesViewModel = new CategoriesViewModel();

        //    foreach (var item in db.Categories)
        //    {
        //        newCategoriesViewModel.Categories.Add(new SelectListItem { Text = item.Name, Value = item.Name });
        //    }
        //    newCategoriesViewModel.Items = allItemsToDisplay;
        //    return View(newCategoriesViewModel);
        //}
    }
}
