using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Contexts;
using RegistryForFinalProject.Enums;
using RegistryForFinalProject.Models;
using RegistryForFinalProject.Models.ViewModels;
using RegistryForFinalProject.Services;
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
            string easyPayNumber = string.Empty;
            if (shoppingCartViewModel.PaymentMethod == Enums.PaymentMethod.EasyPay)
            {
                EasyPayNumberGenerator generator = new EasyPayNumberGenerator();
                easyPayNumber = generator.GenerateEasyPayNumber();
            }
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
                        //this.TempData["NotEnoughQuantity"] = $"Sorry, not enough quantity from item {currentItem.Title} with quantity left - {currentItem.Quantity}, please check in stock items";
                        this.TempData["NotEnoughQuantity"] = string.Format(Constant.NotEnoughQuantity, currentItem.Title, currentItem.Quantity);
                        return RedirectToAction("ShoppingCart");
                    }


                    Order order = new Order
                    {
                        BuyerId = buyer.Id,
                        Date = DateTime.Now,
                        ItemId = item.Id,
                        Price = currentItem.Price * currentItem.Quantity,
                        Quantity = item.Quantity,
                        PaymentMethod = shoppingCartViewModel.PaymentMethod,
                        FirstName = shoppingCartViewModel.FirstName,
                        LastName = shoppingCartViewModel.LastName,
                        Address1 = shoppingCartViewModel.Address,
                        Address2 = shoppingCartViewModel.SecondAddres,
                        City = shoppingCartViewModel.City,
                        State = shoppingCartViewModel.State,
                        Zip = shoppingCartViewModel.Zip,
                        SellerId = currentItem.SellerId,
                        ShippingStatus = ShippingStatus.Processing
                        
                    };
                    if (easyPayNumber != null)
                    {
                        order.EasyPayNumber = easyPayNumber;
                    }
                    db.Orders.Add(order);
                    currentItem.Quantity -= item.Quantity;
                }
                var currentShoppingCart = db.ShoppingCarts.Where(x => x.AccountId == buyer.Id).ToList();
                foreach (var item in currentShoppingCart)
                {
                    item.IsPurchased = true;
                }
                db.SaveChanges();

                if (shoppingCartViewModel.PaymentMethod == Enums.PaymentMethod.Delivery)
                {
                    this.TempData["SuccessfullyPlacedOrder"] = Constant.SuccessfullyPlacedOrder;
                    return RedirectToAction("ShoppingCart");
                }
                else
                {
                    return View("Confirmation",easyPayNumber);
                }

            }
            return View(shoppingCartViewModel);
        }


        public IActionResult Confirmation(string easyPayNumber)
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
