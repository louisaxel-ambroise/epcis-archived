using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class QueryTooComplexException : EpcisException
    {
        public QueryTooComplexException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}