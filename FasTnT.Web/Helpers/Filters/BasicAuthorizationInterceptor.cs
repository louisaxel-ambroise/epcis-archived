using System;
using Ninject.Extensions.Interception;
using System.Web;
using System.Text;
using System.Linq;
using System.Security.Principal;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Model.Users;
using System.Net;
using System.ServiceModel.Web;

namespace FasTnT.Domain.Utils.Aspects
{
    public class BasicAuthorizationInterceptor : IBasicAuthorizationInterceptor
    {
        private readonly IUserRepository _userRepository;

        public BasicAuthorizationInterceptor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            if (TryGetPrincipal(credentials[0], credentials[1], out IPrincipal principal))
            {
                HttpContext.Current.User = principal;
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

        private bool TryGetPrincipal(string username, string password, out IPrincipal principal)
        {
            username = username.Trim();
            password = password.Trim();

            var user = _userRepository.GetByUsername(username);

            if (user != null && user.VerifyPassword(password) && user.Roles.Any(role => role.Id == UserType.ApiUser.Id))
            {
                principal = new GenericPrincipal(new GenericIdentity(username), user.Roles.Select(r => r.Name).ToArray());
                return true;
            }
            else
            {
                principal = null;
                return false;
            }
        }
    }
}