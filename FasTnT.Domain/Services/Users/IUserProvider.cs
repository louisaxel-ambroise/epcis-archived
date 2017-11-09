using FasTnT.Domain.Model.Users;

namespace FasTnT.Domain.Services.Users
{
    public interface IUserProvider
    {
        User GetCurrentUser();
    }
}
