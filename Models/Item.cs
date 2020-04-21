using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(88)]
        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }

        [Required]
        public int SellerId { get; set; }
        public Account Seller { get; set; }


        //public int BuyerId { get; set; }
        //public Account Buyer { get; set; }

        
        [Required(ErrorMessage = Constant.RequiredMessage)]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<RegistryItems> RegistryItems { get; set; } = new List<RegistryItems>();


    }
}
