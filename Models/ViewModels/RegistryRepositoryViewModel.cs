﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class RegistryRepositoryViewModel
    {
        public RegistryRepositoryViewModel()
        {
            Registries = new List<Registry>();
        }
        public List<Registry> Registries { get; set; }
    }
}
