﻿using RegistryForFinalProject.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Models.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        [Required(ErrorMessage = Constant.RequiredEmailError)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = Constant.RequiredMessage)]
        public string Message { get; set; }
    }
}