﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int BuyerId { get; set; }
        public Account Buyer { get; set; }


        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

    }
}
