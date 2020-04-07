using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Constants
{
    public class Constant
    {
        public const string UsernameAlreadyExists = "The username you have entered already exists";
        public const string EmailAlreadyExists = "The email you have entered already exists";
        public const string LogInError = "You must agree to the terms and be above 18 years old.";
        public const string RegisterUsernameLengthError = "The username must be between 5 and 15 characters";
        public const string RegisterUsernameRequiredError = "Username is required";
        public const string RegisterInvalidEmailError = "Please enter a valid email address";
        public const string LogInInvalidUserCredentialsError = "The username and/or password are incorrect";
        public const string RequiredEmailError = "Email is required";
        public const string RequiredPasswardError = "Password is required";
        public const string PasswordComplexityError = "Password must be 8 or more characters, to contain at least one: lower case letter, upper case letter and number";
        public const string RequiredFieldError = "This field is required";

        //Cloudinary
        public const string CLOUD_NAME = "teodorascloud";
        public const string API_KEY = "384475583874649";
        public const string API_SECRET = "fTNzwWElxwVCvz_LsRFoLpDyG7o";

        //Contact form
        public const string RequiredMessage = "it is necessary to add valid content";


    }
}
