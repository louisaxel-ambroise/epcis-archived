using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class EpcisException : Exception
    {
        public EpcisException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}