using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class ForgottenPasswordViewModel
    {

        [EmailAddress(ErrorMessage = Constant.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Constant.RequiredEmailError)]
        public string Email { get; set; }
    }
}
