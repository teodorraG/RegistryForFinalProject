using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class ContactUsViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage = Constant.RequiredEmailError)]
        [EmailAddress(ErrorMessage = Constant.RegisterInvalidEmailError)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Message { get; set; }
    }
}
