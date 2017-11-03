using System;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class QueryTooComplexException : EpcisException
    {
        public QueryTooComplexException(string message) : base(message)
        {
        }
    }
}
