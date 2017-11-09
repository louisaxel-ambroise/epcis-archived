using FasTnT.Domain.Model.Dashboard;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Utils.Aspects;
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

        [CommitTransaction]
        public virtual void SetPassword(Guid userId, string previous, string updated)
        {
            //TODO: set user password (salt/hash)
            throw new NotImplementedException();
        }

        [CommitTransaction]
        public virtual void SetPreferredLanguage(Guid userId, string language)
        {
            var user = _userRepository.Load(userId);
            user.PreferredLanguage = language;
        }
    }
}
