using System;

namespace FasTnT.Domain.Utils
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
