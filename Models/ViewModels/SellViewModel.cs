using Microsoft.AspNetCore.Mvc.Rendering;
using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class SellViewModel
    {
        public SellViewModel()
        {
           
        }
        public string Image { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Title { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = Constant.RequiredMessage)]
        public List<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Description { get; set; }

        
        public int Quantity { get; set; }

        public Category SelectedCategory { get; set; }
    }
}
