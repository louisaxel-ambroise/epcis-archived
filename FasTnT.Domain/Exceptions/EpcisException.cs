using System;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class EpcisException : Exception
    {
        public EpcisException(string message) : base(message)
        {
        }
    }
}
