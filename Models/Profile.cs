using RegistryForFinalProject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public Gender Gender { get; set; }

        public string Address { get; set; }

        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least one: lower case letter, upper case letter and number")]
        public string NewPassword { get; set; }

        public string Orders { get; set; } // Order Orders

        public string Offers { get; set; } // Offer Offers

    }
}
