using System.Text.RegularExpressions;

namespace TransportationAnalyticsHub.MVVM.Model
{
    public static class DataValidator
    {
        private const string DecimalValidation = @"^[0-9]+([,.][0-9]{0,4})?$";
        private const string NameValidationStr = @"^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]{0,20}$";
        private const string IntValidationStr = @"^[0-9]{0,20}$";
        private const string LatinCharValidationStr = @"^[0-9A-Za-z]{0,20}$";

        public static bool ValidateDecimal(string text) => Regex.Match(text, DecimalValidation).Success;

        public static bool ValidateName(string text) => Regex.Match(text, NameValidationStr).Success;

        public static bool ValidateInt(string text) => Regex.Match(text, IntValidationStr).Success;

        public static bool ValidatePesel(string text) => ValidateInt(text) && text.Length == 11;
        public static bool ValidateOnlyLatin(string text) => Regex.Match(text, LatinCharValidationStr).Success;
        public static bool ValidateRegistration(string text) => ValidateOnlyLatin(text) && text.Length <= 8;

        public static bool ValidateAge18(string text)
        {
            var birthdate = DataConverter.ConvertToDateTime(text);
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
