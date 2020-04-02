using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Orders
    {
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public DateTime Date { get; set; }
    }
}
