using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class LogInViewModel
    {

        [Required(ErrorMessage = Constant.RegisterUsernameRequiredError)]
        public string UserName { get; set; }

        [Required(ErrorMessage = Constant.RequiredPasswardError)]
        public string Password { get; set; }
    }
}
