using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers
{
    public class SellController: Controller
    {
        public IActionResult Sell()
        {

            return View();
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
