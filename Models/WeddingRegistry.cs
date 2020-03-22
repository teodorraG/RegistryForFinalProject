using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class WeddingRegistry
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string BrideName { get; set; }

        [Required]
        public string GroomName { get; set; }

        [Required]
        public string CityOfEvent { get; set; }
        
        [Required]
        public DateTime DateOfEvent { get; set; }
    }
}
