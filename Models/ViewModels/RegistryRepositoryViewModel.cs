using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class RegistryRepositoryViewModel
    {
        public RegistryRepositoryViewModel()
        {
            BabyRegistries = new List<Registry>();
            WeddingRegistries = new List<Registry>();
            BirthdayRegistries = new List<Registry>();
        }
        public List<Registry> BabyRegistries { get; set; }
        public List<Registry> WeddingRegistries { get; set; }
        public List<Registry> BirthdayRegistries { get; set; }
    }
}
