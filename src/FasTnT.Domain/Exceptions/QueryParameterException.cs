using System;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class QueryParameterException : EpcisException
    {
        public QueryParameterException(string parameterName) : base($"Unknown parameter name: '{parameterName}'")
        {
        }
    }
}
