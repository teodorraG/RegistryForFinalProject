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
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage ="Password must contain at least one: lower case letter, upper case letter and number")]
        public string Password { get; set; }

        public Role Role { get; set; }

    }
}
