using System;

namespace FasTnT.Domain.Exceptions
{
    public class QueryParameterException : Exception
    {
        public string ParameterName { get; set; }

        public QueryParameterException(string parameterName) : base($"Unknown parameter name: '{parameterName}'")
        {
            ParameterName = parameterName;
        }
    }
}
