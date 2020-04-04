using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class CategoriesController:Controller
    {
        public IActionResult Categories()
        {

            return View();
        }


        [HttpPost]
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
