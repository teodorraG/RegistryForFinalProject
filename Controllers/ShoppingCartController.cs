using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Controllers 
{
    public class ShoppingCartController : Controller
    {
        private readonly RegistryDbContext db = new RegistryDbContext();

        public ShoppingCartController(RegistryDbContext registryContext)
        {
            db = registryContext;
        }
        public IActionResult ShoppingCart()
        {
            var currentUserName = HttpContext.Session.GetString("CurrentUser");
            var userId = db.Accounts.FirstOrDefault(x => x.UserName == currentUserName).Id;
            var currentShoppingCartItems = db.ShoppingCarts.Where(x => x.AccountId == userId).ToList();
            var items = new List<Item>();
            foreach (var item in currentShoppingCartItems)
            {
                items.Add(db.Items.FirstOrDefault(x=>x.Id == item.ItemId));
            }
            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel { Items = items };
            return View(shoppingCartViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {

            if (ModelState.IsValid)
            {
                return View();
            }
            return View(shoppingCartViewModel);
        }


        public IActionResult Confirmation()
        {

            return View("Confirmation");
        }

        public IActionResult RemoveItem(int id)
        {
            var userName = HttpContext.Session.GetString("CurrentUser");
            var account = db.Accounts.FirstOrDefault(x => x.UserName == userName);
            var item = db.Items.FirstOrDefault(x => x.Id == id);
            var itemToRemoveFromCart = db.ShoppingCarts.FirstOrDefault(x => x.ItemId == item.Id && x.AccountId == account.Id);
            db.ShoppingCarts.Remove(itemToRemoveFromCart);
            db.SaveChanges();
            return RedirectToAction("ShoppingCart", "ShoppingCart");
        }

    }
}
