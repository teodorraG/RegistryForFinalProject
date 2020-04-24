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

        public const string NO_IMAGE = "heltc6lmr73m5gtgbv04";

        //Contact form
        public const string RequiredMessage = "It is necessary to add valid content";

        //TempData:

        //Contact
        public const string SuccessfullySentEmail = "Your message was sent successfully";

        //AccountController
        public const string SuccessfullyRegistered = "Successfully registered. Please Sign In to continue!";
        public const string SentEmail = "An email has been sent to you.";
        public const string NotMatchingEmail = "Sorry, this email doesn't exist";

        //CategoriesController
        public const string Search = "No entered content";
        public const string AlreadyInCart = "This item is already in your shopping cart";
        public const string AddedItemToRegistry = "Successfully added item to registry";

        //ProfileController

        public const string InvalidGender = "Select your gender type";
        public const string MadeChanges = "Your changes have been saved";
        public const string NoDataEntered = "Enter valid data";
        public const string MatchingPassword = "The new password matches current password";
        public const string MadeChangesToPass = "Your changes have been saved";
        public const string ErrorChanges = "Make sure your password is valid";
        public const string ErrorComplexity = "Password must be 8 or more characters, to contain at least one: lower case letter, upper case letter and number";
        public const string PermanentlyDeletedItem = "Permanently deleted item";
        public const string SuccessfullyEdited = "Your item was successfully edited";
        public const string SuccessfullyChangedStatus = "The status has been changed successfully";

        //RegistryController
        public const string SuccessfullyCreatedRegistry = "Successfully created registry!";
        public const string IncorrectRegistryForm = "Incorrect registry form";
        public const string NotFoundRegistry = "Not found registry";
        public const string NotFoundRegistryForm = "Not found registry, make sure the form is filled correctly";

        //SellController
        public const string SuccessfullyListed = "Successfully listed item!";
        public const string NoCategorySelected = "Select category";

        //ShoppingCartController
        public const string NotEnoughQuantity = "Sorry, not enough quantity from item {0} with quantity left: {1}, please check in stock items";
        public const string SuccessfullyPlacedOrder = "Your order has been placed successfully";

    }
}
