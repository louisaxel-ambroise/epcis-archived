using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class SecurityException : EpcisException
    {
        public SecurityException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}