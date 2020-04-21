using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class RegistryItems
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int RegistryId { get; set; }

        public Registry Registry { get; set; }
    }
}
