using System;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class ImplementationException : Exception
    {
        public ImplementationException(string parameterName): base($"Processing of parameter '{parameterName}' is not implemented.")
        {

        }
    }
}
