using System;
using System.Collections.Generic;

namespace SampleEcomStoreApi.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string> ValidationErrors { get; private set; }

        public ValidationException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string>();
        }

        public ValidationException(string message, Dictionary<string, string> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors ?? new Dictionary<string, string>();
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
            ValidationErrors = new Dictionary<string, string>();
        }
    }
}
