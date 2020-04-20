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
                if (!item.IsPurchased)
                {
                    items.Add(db.Items.FirstOrDefault(x => x.Id == item.ItemId));
                }
            }
            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel { Items = items };
            return View(shoppingCartViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {
            for (int i = 0; i < shoppingCartViewModel.Items.Count; i++)
            {
                var item = db.Items.FirstOrDefault(x => x.Id == shoppingCartViewModel.Items[i].Id);
                shoppingCartViewModel.Items[i].Title = item.Title;
                shoppingCartViewModel.Items[i].Price = item.Price;
                shoppingCartViewModel.Items[i].Description = item.Description;
                shoppingCartViewModel.Items[i].SellerId = item.SellerId;
                shoppingCartViewModel.Items[i].CategoryId = item.CategoryId;
            }
            ModelState.Clear();
            TryValidateModel(shoppingCartViewModel);
            if (ModelState.IsValid)
            {
                var currentUserName = HttpContext.Session.GetString("CurrentUser");
                var buyer = db.Accounts.FirstOrDefault(x => x.UserName == currentUserName);
                foreach (var item in shoppingCartViewModel.Items)
                {
                    var currentItem = db.Items.FirstOrDefault(x => x.Id == item.Id);
                    if (currentItem.Quantity < item.Quantity)
                    {
                        this.TempData["NotEnoughQuantity"] = "Sorry, not enough quantity, please check in stock items";
                        return RedirectToAction("ShoppingCart");
                    }
                    Order order = new Order
                    {
                        BuyerId = buyer.Id,
                        Date = DateTime.Now,
                        ItemId = item.Id,
                        Price = currentItem.Price * currentItem.Quantity,
                        Quantity = item.Quantity
                    };
                    db.Orders.Add(order);
                    
                    currentItem.Quantity -= item.Quantity;

                    //if (currentItem.Quantity <= 0)
                    //{
                    //    var deleteItem = db.Categories.FirstOrDefault(x => x.Id == currentItem.Id);
                    //    db.Categories.Remove(deleteItem);
                    //        db.SaveChanges();
                    //}
                }
                var currentShoppingCart = db.ShoppingCarts.Where(x => x.AccountId == buyer.Id).ToList();
                foreach (var item in currentShoppingCart)
                {
                    item.IsPurchased = true;
                }
                db.SaveChanges();
                return View("Confirmation");
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
