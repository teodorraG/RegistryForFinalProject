using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; set; }

        public List<Item> Items { get; set; }
    }
}
