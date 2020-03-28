using RegistryForFinalProject.Enums;
using RegistryForFinalProject.ErrorMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Range(5,15,ErrorMessage =Errors.RegisterUsernameLengthError)]
        [Required(ErrorMessage = Errors.RegisterUsernameRequiredError)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = Errors.RegisterInvalidEmailError)]
        [Required(ErrorMessage = Errors.RequiredEmailError)]
        public string Email { get; set; }

        [Required(ErrorMessage = Errors.RequiredPasswardError)]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = Errors.PasswordComplexityError)]
        public string Password { get; set; }

        public Role Role { get; set; }

    }
}
