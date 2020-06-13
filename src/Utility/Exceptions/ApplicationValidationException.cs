using System;
namespace Utility.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException(string message):base(message)
        {
        }
    }
}