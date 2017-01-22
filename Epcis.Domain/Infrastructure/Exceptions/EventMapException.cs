using System;

namespace Epcis.Domain.Exceptions
{
    public class EventMapException : Exception
    {
        public EventMapException(string message) : base(message)
        {
        }
    }
}