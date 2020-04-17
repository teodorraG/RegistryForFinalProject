using RegistryForFinalProject.Enums;
using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Range(5,15,ErrorMessage =Constant.RegisterUsernameLengthError)]
        [Required(ErrorMessage = Constant.RegisterUsernameRequiredError)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = Constant.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Constant.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constant.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = Constant.PasswordComplexityError)]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public List<Item> ItemsSold { get; set; } = new List<Item>();
        //public List<Item> ItemsBought { get; set; } = new List<Item>();
        public List<Order> Orders { get; set; } = new List<Order>();

        public List<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();


    }
}
