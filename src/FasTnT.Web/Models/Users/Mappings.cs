using FasTnT.Domain.Model.Dashboard;
using FasTnT.Domain.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.Models.Users
{
    public static class Mappings
    {
        public static IEnumerable<ApiUserViewModel> MapToApiUserViewModel(this IQueryable<User> userList)
        {
            return userList.Select(u => new ApiUserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Type = u.Role.Name,
                LastLogon = u.LastLogOn,
                IsActive = u.IsActive
            });
        }

        public static ApiUserViewModel MapToApiUserViewModel(this User user)
        {
            return new ApiUserViewModel
            {
                Id = user.Id,
                LastLogon = user.LastLogOn,
                Name = user.Name,
                IsActive = user.IsActive
            };
        }

        public static UserDetailsViewModel MapToViewModel(this UserDetail details)
        {
            return new UserDetailsViewModel
            {
                Name = details.Name,
                LastLogOn = details.LastLogOn
            };
        }
    }
}