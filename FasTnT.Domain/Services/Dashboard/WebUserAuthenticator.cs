using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Utils;

namespace FasTnT.Domain.Services.Dashboard
{
    public class WebUserAuthenticator : IWebUserAuthenticator
    {
        private readonly IUserRepository _userRepository;

        public WebUserAuthenticator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            User user;

            try
            {
                user = _userRepository.GetByUsername(username);
            }
            catch
            {
                throw new UserAuthenticationException(UserAuthenticationException.Failure.UnknownError);
            }

            if(user == null)
            {
                throw new UserAuthenticationException(UserAuthenticationException.Failure.UnknownUser);
            }
            if (!user.Roles.Contains(UserType.DashboardUser))
            {
                throw new UserAuthenticationException(UserAuthenticationException.Failure.AccessDenied);
            }
            if(!user.VerifyPassword(password))
            {
                throw new UserAuthenticationException(UserAuthenticationException.Failure.WrongPassword);
            }

            user.LastLogOn = SystemContext.Clock.Now;
            return user;
        }
    }
}
