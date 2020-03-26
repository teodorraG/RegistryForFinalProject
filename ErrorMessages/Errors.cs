﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.ErrorMessages
{
    public class Errors
    {
        public const string UsernameAlreadyExists = "The username you have entered already exists";
        public const string EmailAlreadyExists = "The email you have entered already exists";
        public const string LogInError = "You must agree to the terms and be above 18 years old.";
        public const string RegisterUsernameLengthError = "The username must be between 5 and 15 characters";
        public const string RegisterUsernameRequiredError = "UserName is required";
        public const string RegisterInvalidEmailError = "Please enter a valid email address";
        public const string LogInInvalidUserCredentialsError = "The username and/or password are incorrect";
    }
}
