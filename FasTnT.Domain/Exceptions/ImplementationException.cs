using System;

namespace FasTnT.Domain.Exceptions
{
    public class ImplementationException : Exception
    {
        public ImplementationException(string parameterName): base($"Processing of parameter '{parameterName}' is not implemented.")
        {

        }
    }
}
