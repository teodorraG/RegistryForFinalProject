using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class SellController: Controller
    {
        private readonly RegistryDbContext db = new RegistryDbContext();


        public SellController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult Sell()
        {

            var categories = db.Categories.ToList();
            SellViewModel sellViewModel = new SellViewModel();
            return View(sellViewModel);
        }
        [HttpPost]
        public IActionResult Sell(SellViewModel sellViewModel)
        {

            if (ModelState.IsValid)
            {
                return View("Sell");
            }

            return View(sellViewModel);
        }
    }
}
