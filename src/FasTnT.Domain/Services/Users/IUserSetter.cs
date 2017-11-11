using FasTnT.Domain.Model.Users;

namespace FasTnT.Domain.Services.Users
{
    public interface IUserSetter
    {
        void SetCurrentUser(User user);
    }
}
