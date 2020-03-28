using RegistryForFinalProject.ErrorMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class AccountViewModel
    {
        [MinLength(5, ErrorMessage = Errors.RegisterUsernameLengthError)]
        [MaxLength(15, ErrorMessage = Errors.RegisterUsernameLengthError)]
        [Required(ErrorMessage = Errors.RegisterUsernameRequiredError)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = Errors.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Errors.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Errors.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least one: lower case letter, upper case letter and number")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
