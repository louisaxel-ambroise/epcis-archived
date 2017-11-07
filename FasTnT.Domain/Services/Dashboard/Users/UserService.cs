using FasTnT.Domain.Model.Dashboard;
using FasTnT.Domain.Repositories;
using System;
using System.Linq;

namespace FasTnT.Domain.Services.Dashboard.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDetail GetDetails(string userName)
        {
            var user = _userRepository.Query().Single(u => u.Name == userName);

            return new UserDetail { Name = user.Name, LastLogOn = user.LastLogOn.Value };
        }

        public void SetPassword(string userName, string previous, string updated)
        {
            throw new NotImplementedException();
        }
    }
}
