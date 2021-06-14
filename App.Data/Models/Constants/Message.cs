namespace App.Data.Model
{
    public static class Message
    {
        public const string SomethingWentWrong = "Something went wrong. Please refresh the page or try again later.";
        public const string Unauthorized = "Unauthorized Request.";
        public const string InvalidLogin = "Invalid Username or Password.";
        public const string NotFound = "The resource you are looking for is not found.";
        public const string InvalidOperation = "Invalid Operation.";
        public const string RecordExist = "Record already exist.";
        public const string AddSuccess = "Record added successfully.";
        public const string UpdateSuccess = "Record updated successfully.";
        public const string UpdateError = "The record you are trying to update is not found.";
        public const string DeleteSuccess = "Record deleted successfully.";
        public const string DeleteError = "The resource you are trying to delete is not found.";
        public const string InvalidInfoCode = "Invalid infocode {0}.";

        public const string FirstNameValidationError = "First Name should not be more than 100 characters.";
        public const string MiddleNameValidationError = "Middle Name should not be more than 100 characters.";
        public const string LastNameValidationError = "Last Name should not be more than 100 characters.";
        public const string UserNameValidationError = "User Name should not be more than 100 characters.";
        public const string EmailValidationError = "Invalid email address.";
        public const string PasswordValidationError = "Password length should be between 6 to 15 and must contain one lower case, upper case and digit.";

        public const string NameLengthError = "Name should not be greater than 100 characters.";
        public const string StringLength50Error = "Name should not be greater than 50 characters.";
        public const string StringLength30Error = "Name should not be greater than 30 characters.";
        public const string CodeLength5Error = "Code length should not be greater than 5 characters.";
        public const string CodeLength3Error = "Code length should not be greater than 3 characters.";
        public const string MaxLength10Error = "Max 10 char supported for this field";
        public const string MaxLength12Error = "Max 12 char supported for this field";
        public const string MaxLength20Error = "Max 20 char supported for this field";
        public const string MaxLength50Error = "Max 50 char supported for this field";
        public const string MaxLength100Error = "Max 100 char supported for this field";
        public const string MaxLength200Error = "Max 200 char supported for this field";

        public const string CustomerSettingDoesNotExist = "No settings found for your account. Please contact support team.";
        public const string ServiceTemplateDoesNotExist = "No service is assigned to your account. Please contact support team.";

        #region 'Lead'
        public const string FistNameRequired = "Fist name is required";
        public const string EmailIsRequired = "Email is required";
        public const string InvalidEmail = "Email is invalid";
        #endregion

    }
}
