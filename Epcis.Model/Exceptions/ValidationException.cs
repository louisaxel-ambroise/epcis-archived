using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class ValidationException : EpcisException
    {
        public ValidationException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}