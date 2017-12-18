using System;

namespace Sematext.Exceptions
{
    public class SematextValidationException : Exception
    {
        public SematextValidationException(string message) : base(message)
        {
        }
    }
}