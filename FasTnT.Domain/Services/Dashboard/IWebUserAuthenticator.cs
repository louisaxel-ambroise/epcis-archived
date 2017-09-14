using FasTnT.Domain.Model.Users;

namespace FasTnT.Domain.Services.Dashboard
{
    public interface IWebUserAuthenticator
    {
        User Authenticate(string username, string password);
    }
}
