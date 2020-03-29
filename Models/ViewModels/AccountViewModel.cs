using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RegistryForFinalProject.Constants;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class AccountViewModel
    {
        [MinLength(5, ErrorMessage = Constant.RegisterUsernameLengthError)]
        [MaxLength(15, ErrorMessage = Constant.RegisterUsernameLengthError)]
        [Required(ErrorMessage = Constant.RegisterUsernameRequiredError)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = Constant.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Constant.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Constant.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least one: lower case letter, upper case letter and number")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
