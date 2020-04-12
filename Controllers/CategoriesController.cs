using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class CategoriesController:Controller
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
            categoriesViewModel.Categories = categories;
            categoriesViewModel.Items = items;
            foreach (var item in categoriesViewModel.Items)
            {
                //if (item.Description.Length >= 176)
                //{
                //    item.Description = item.Description.Substring(0, 193);
                //    item.Description += " . . .";
                //}
                //if (item.Title.Length >= 44)
                //{
                //    item.Title = item.Description.Substring(0, 41);
                //    item.Title += "...";
                //}
                
            }
            return View(categoriesViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Categories(CategoriesViewModel categoriesViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("Categories");
            }

            return View(categoriesViewModel);
        }
    }
}
