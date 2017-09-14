using FasTnT.Domain.Model.Users;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.Models.Users
{
    public static class Mappings
    {
        public static IEnumerable<UserViewModel> MapToUserViewModel(this IQueryable<User> userList)
        {
            return userList.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Type = string.Join("_", u.Roles.OrderBy(x => x.Id).Select(r => r.Name)),
                LastLogon = u.LastLogOn
            });
        }
    }
}