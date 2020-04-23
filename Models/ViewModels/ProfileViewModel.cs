using RegistryForFinalProject.Constants;
using RegistryForFinalProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            Offers = new List<Item>();
            Orders = new List<Order>();
        }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string CurrentPassword { get; set; }

        [NotMapped]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = Constant.PasswordComplexityError)]
        public string NewPassword { get; set; }

        public List<Item> Offers { get; set; }

        public List<Order> Orders { get; set; }

        public ShippingStatus ShippingStatus { get; set; }

    }
}
