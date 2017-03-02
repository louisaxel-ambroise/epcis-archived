using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class ImplementationException : EpcisException
    {
        public ImplementationException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}