using System;

namespace FasTnT.Domain.Utils
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}