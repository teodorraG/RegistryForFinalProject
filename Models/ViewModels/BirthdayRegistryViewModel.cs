using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class BirthdayRegistryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }
    }
}
