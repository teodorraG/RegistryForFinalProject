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
        [Key]
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = Constant.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Constant.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constant.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = Constant.PasswordComplexityError)]
        public string NewPassword { get; set; }

        [NotMapped]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
