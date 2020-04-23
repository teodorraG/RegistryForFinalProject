using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class PreviewItemViewModel
    {
        public Item Item { get; set; }

        public List<Registry> Registries { get; set; }

        public string SelectedValue { get; set; }

    }
}
