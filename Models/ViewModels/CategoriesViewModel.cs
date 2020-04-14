using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public string SelectedCategory { get; set; }

        public List<Item> Items { get; set; }

    }
}
