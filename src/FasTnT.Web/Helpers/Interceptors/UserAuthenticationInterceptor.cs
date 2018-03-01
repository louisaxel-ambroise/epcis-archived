using System;
using Ninject.Extensions.Interception;
using System.Web;
using System.Text;
using System.Linq;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Services.Users;
using System.Net;
using FasTnT.Domain.Utils;

namespace FasTnT.Web.Helpers.Attributes
{
    public class UserAuthenticationInterceptor : IAuthenticateUserInterceptor
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSetter _userSetter;

        const string UrlAuthKey = "token";
        const string HeaderKey = "Authorization";

        protected virtual Func<Exception> UnauthorizedException => () => new HttpException((int)HttpStatusCode.Unauthorized, "Unauthorized.");

        public UserAuthenticationInterceptor(IUserRepository userRepository, IUserSetter userSetter)
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
                throw UnauthorizedException();
            }
        }

        private bool Authenticate()
        {
            string[] credentials;

            if (HttpContext.Current.Request.Headers.AllKeys.Contains(HeaderKey))
            {
                var authHeader = HttpContext.Current.Request.Headers[HeaderKey];
                credentials = ParseAuthHeader(authHeader);
            }
            else if (HttpContext.Current.Request.QueryString.AllKeys.Contains(UrlAuthKey))
            {
                var authQuery = HttpContext.Current.Request.QueryString[UrlAuthKey];
                credentials = ParseQueryString(authQuery);
            }
            else
            {
                return false;
            }

            if (TryGetPrincipal(credentials[0], credentials[1], out User user))
            {
                user.LastLogOn = SystemContext.Clock.Now;

                _userSetter.SetCurrentUser(user);
                return true;
            }

            return false;
        }

        private string[] ParseAuthHeader(string authHeader)
        {
            if (authHeader == null || authHeader.Length == 0 || !authHeader.StartsWith("Basic")) return null;

            return ParseQueryString(authHeader.Substring(6));
        }

        private string[] ParseQueryString(string authHeader)
        {
            if (authHeader == null || authHeader.Length == 0) return null;

            var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader)).Split(':');

            return (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[0])) ? null : credentials;
        }

        private bool TryGetPrincipal(string username, string password, out User user)
        {
            username = username.Trim();
            password = password.Trim();

            user = _userRepository.GetByUsername(username);

            return user != null && user.VerifyPassword(password) && user.Role.Equals(UserType.ApiUser) && user.IsActive;
        }
    }
}