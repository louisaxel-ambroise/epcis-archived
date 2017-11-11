using System;

namespace FasTnT.Domain.Exceptions
{
    [Serializable]
    public class NoSuchNameException : EpcisException
    {
        public NoSuchNameException(string message) : base(message)
        {
        }
    }
}
