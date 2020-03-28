using RegistryForFinalProject.ErrorMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class BabyRegistryViewModel
    {

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public string Name { get; set; }

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public string City { get; set; }

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public DateTime DateOfEvent { get; set; }
    }
}
