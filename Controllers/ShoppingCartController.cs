using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers 
{
    public class ShoppingCartController : Controller
    {
       
        public IActionResult ShoppingCart()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {

            if (ModelState.IsValid)
            {
                return View();
            }
            return View(shoppingCartViewModel);
        }

    }
}
