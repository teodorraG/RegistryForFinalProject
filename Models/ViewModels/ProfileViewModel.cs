using RegistryForFinalProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string NewPassword { get; set; }

        [NotMapped]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public string Orders { get; set; } // Order Orders

        public string Offers { get; set; } // Offer Offers

    }
}
