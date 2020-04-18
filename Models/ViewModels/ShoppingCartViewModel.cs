using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            Items = new List<Item>();
        }
        public List<Item> Items { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string LastName  { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string Address { get; set; }

        public string SecondAddres { get; set; }

        [Required(ErrorMessage = Constant.RequiredFieldError)]
        public string City { get; set; }

        public string State { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public int Zip { get; set; }
    }
}
