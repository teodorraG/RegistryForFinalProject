﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class OrdersViewModel
    {
        public OrdersViewModel()
        {
            Items = new List<Item>();
        }
        public List<Item> Items { get; set; }

    }
}
