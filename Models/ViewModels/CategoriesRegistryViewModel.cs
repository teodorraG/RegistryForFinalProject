using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class CategoriesRegistryViewModel
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public int AccountId { get; set; }

        public int RegistryId { get; set; }
    }
}
