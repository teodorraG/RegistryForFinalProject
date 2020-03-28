using RegistryForFinalProject.ErrorMessages;
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

        [EmailAddress(ErrorMessage = Errors.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Errors.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Errors.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = Errors.PasswordComplexityError)]
        public string NewPassword { get; set; }

        [NotMapped]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
