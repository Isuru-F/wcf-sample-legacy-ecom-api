using System.Collections.Generic;
using SampleEcomStoreApi.Common.Exceptions;

namespace SampleEcomStoreApi.Common.Validation
{
    public static class ValidationHelper
    {
        public static void ValidateObject<T>(T objectToValidate) where T : class
        {
            if (objectToValidate == null)
            {
                throw new ValidationException("Object cannot be null");
            }

            // Basic validation - in a real implementation you would use Enterprise Library
            // For now, just check for null
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^[\+]?[1-9][\d]{0,15}$");
        }
    }
}
