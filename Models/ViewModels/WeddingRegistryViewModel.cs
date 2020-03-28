using RegistryForFinalProject.ErrorMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class WeddingRegistryViewModel
    {

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public string BrideName { get; set; }

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public string GroomName { get; set; }

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public string CityOfEvent { get; set; }

        [Required(ErrorMessage = Errors.RequiredFieldError)]
        public DateTime DateOfEvent { get; set; }
    }
}
