﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class OffersViewModel
    {
        public OffersViewModel()
        {
            Items = new List<Item>();
        }
        public List<Item> Items { get; set; }
    }
}
