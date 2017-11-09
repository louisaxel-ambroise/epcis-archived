using System;
using Ninject.Extensions.Interception;
using System.Web;
using System.Text;
using System.Linq;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Model.Users;
using System.Net;
using System.ServiceModel.Web;
using FasTnT.Domain.Services.Users;

namespace FasTnT.Domain.Utils.Aspects
{
    public class AuthenticateUserInterceptor : IAuthenticateUserInterceptor
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSetter _userSetter;

        public AuthenticateUserInterceptor(IUserRepository userRepository, IUserSetter userSetter)
        {
            _userRepository = userRepository;
            _userSetter = userSetter;
        }

        public void Intercept(IInvocation invocation)
        {
            if(Authenticate())
            {
                invocation.Proceed();
            }
            else
            {
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }

        private bool Authenticate()
        {
            if (!HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization")) return false;

            var authHeader = HttpContext.Current.Request.Headers["Authorization"];
            var credentials = ParseAuthHeader(authHeader);

            if (TryGetPrincipal(credentials[0], credentials[1], out User user))
            {
                _userSetter.SetCurrentUser(user);
                return true;
            }

            return false;
        }

        private string[] ParseAuthHeader(string authHeader)
        {
            if (authHeader == null || authHeader.Length == 0 || !authHeader.StartsWith("Basic")) return null;

            var base64Credentials = authHeader.Substring(6);
            var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(base64Credentials)).Split(new char[] { ':' });

            return (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[0])) ? null : credentials;
        }

        private bool TryGetPrincipal(string username, string password, out User user)
        {
            username = username.Trim();
            password = password.Trim();

            user = _userRepository.GetByUsername(username);

            return user != null && user.VerifyPassword(password) && user.Roles.Any(role => role.Id == UserType.ApiUser.Id);
        }
    }
}