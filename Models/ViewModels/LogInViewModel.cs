using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class LogInViewModel
    {
        //[MinLength(5, ErrorMessage = "The username must be between 5 and 15 characters")]
        //[MaxLength(15, ErrorMessage = "The username must be between 5 and 15 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression(pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Password must contain at least one: lower case letter, upper case letter and number")]
        public string Password { get; set; }
    }
}
