namespace PCS.Core.Utils.Concrete
{
    public static class ValidationErrorMessages
    {
        public const string EmptyError = "Please ensure you have entered your {PropertyName}.";
        public const string MaxLengthError = " The length of {PropertyName} must be {MaxLength} characters or fewer." +
            "You entered {TotalLength} characters.";
        public const string MinLengthError = "The length of {PropertyName}  must be at least {MinLength} characters." +
            "You entered {TotalLength} characters.";
        public const string MaxMinLengthError = "{PropertyName} must be between {MinLength} and {MaxLength} characters. " +
            "You entered {TotalLength} characters.";
        public const string LengthError = "{PropertyName} must be {Length} characters. " +
            "You entered {TotalLength} characters.";
        public const string EmailError = "Please ensure you have entered valid {PropertyName}.";
        public const string PropertyNotValidError = "{PropertyName} is not valid.";
    }
}
