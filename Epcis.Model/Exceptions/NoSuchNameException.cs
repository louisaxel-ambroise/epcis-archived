using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class NoSuchNameException : EpcisException
    {
        public NoSuchNameException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}