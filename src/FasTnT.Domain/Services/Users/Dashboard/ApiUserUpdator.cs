using FasTnT.Domain.Repositories;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Domain.Utils.Security;
using System;

namespace FasTnT.Domain.Services.Users.Dashboard
{
    public class ApiUserUpdator : IApiUserUpdator
    {
        private readonly IUserRepository _userRepository;

        public ApiUserUpdator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [CommitTransaction]
        public virtual void Update(Guid id, string name, bool isActive, string password = null)
        {
            var user = _userRepository.Load(id);

            user.Name = name;
            user.IsActive = isActive;

            if (!string.IsNullOrEmpty(password))
            {
                var salt = BCrypt.GenerateSalt();
                var hashedPassword = BCrypt.HashPassword(password, salt);

                user.PasswordSalt = salt;
                user.PasswordHash = hashedPassword;
            }
        }
    }
}
