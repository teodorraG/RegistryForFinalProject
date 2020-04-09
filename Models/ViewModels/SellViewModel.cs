using Microsoft.AspNetCore.Mvc;
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
        [HiddenInput]
        public string Image1 { get; set; }

        [HiddenInput]
        public string Image2 { get; set; }

        [HiddenInput]
        public string Image3 { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Title { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = Constant.RequiredMessage)]
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>
        {
            new SelectListItem{Text = "Baby & Kids", Value = "Baby & Kids"},
            new SelectListItem{Text = "Beauty & Personal Care", Value = "Beauty & Personal Care"},
            new SelectListItem{Text = "Books", Value = "Books"},
            new SelectListItem{Text = "Electronics & Computers", Value = "Electronics & Computers"},
            new SelectListItem{Text = "Health & Household", Value = "Health & Household"},
            new SelectListItem{Text = "Home & Kitchens", Value = "Home & Kitchen"},
            new SelectListItem{Text = "Pet Supplies", Value = "Pet Supplies"},
            new SelectListItem{Text = "Sports & Outdoors", Value = "Sports & Outdoors"},
            new SelectListItem{Text = "Tools & Home Improvement", Value = "Tools & Home Improvement"},
            new SelectListItem{Text = "Toys & Games", Value = "Toys & Games"},
        };

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Description { get; set; }

        
        public int Quantity { get; set; }

        public string SelectedCategory { get; set; }
    }
}
