using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Utils.Aspects;

namespace FasTnT.Domain.Services.Dashboard
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [CommitTransaction]
        public virtual User Authenticate(string username, string password)
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

            if (user == null)
            {
                throw new UserAuthenticationException(UserAuthenticationException.Failure.UnknownUser);
            }
            if (!user.Role.Equals(UserType.DashboardUser))
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
