using System;

namespace FasTnT.Domain.Services.Users.Dashboard
{
    public interface IApiUserUpdator
    {
        void Update(Guid id, string name, bool isActive, string password = null);
    }
}
