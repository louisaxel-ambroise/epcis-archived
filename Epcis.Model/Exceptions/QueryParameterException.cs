using System;

namespace Epcis.Model.Exceptions
{
    [Serializable]
    public class QueryParameterException : EpcisException
    {
        public QueryParameterException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}