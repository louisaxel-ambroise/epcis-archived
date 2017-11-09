using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Users;
using FasTnT.Web.Session;
using System;
using System.Web;

namespace FasTnT.Web.Helpers.Users
{
    public class HttpUserContainer : IUserProvider, IUserSetter
    {
        private static string CurrentUserKey = "CurrentUser";

        private readonly IUserRepository _userRepository;

        public HttpUserContainer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetCurrentUser()
        {
            return GetFromCurrentContext() ?? GetAuthenticatedUser();
        }

        public void SetCurrentUser(User user)
        {
            HttpContext.Current.Items[CurrentUserKey] = user;
        }

        private User GetFromCurrentContext()
        {
            return HttpContext.Current.Items[CurrentUserKey] as User;
        }

        private User GetAuthenticatedUser()
        {
            var currentWebUser = UserSession.Current;
            if (currentWebUser == null) return null;

            var user = _userRepository.Load(currentWebUser.Id);
            if (user == null) throw new InvalidOperationException($"User '{currentWebUser.Id}' cannot be found in the database");

            SetCurrentUser(user);
            return user;
        }
    }
}