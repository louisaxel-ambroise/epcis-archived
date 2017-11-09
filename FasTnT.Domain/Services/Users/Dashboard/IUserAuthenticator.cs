using FasTnT.Domain.Model.Users;

namespace FasTnT.Domain.Services.Dashboard
{
    public interface IUserAuthenticator
    {
        User Authenticate(string username, string password);
    }
}
