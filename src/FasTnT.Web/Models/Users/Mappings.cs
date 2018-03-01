using FasTnT.Domain.Model.Subscriptions;
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
                LastLogon = u.LastLogOn,
                IsActive = u.IsActive,
                SubscriptionCount = u.Subscriptions.Count()
            });
        }

        public static ApiUserDetailViewModel MapToApiUserViewModel(this User user)
        {
            return new ApiUserDetailViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastLogon = user.LastLogOn,
                IsActive = user.IsActive,
                Subscriptions = user.Subscriptions.MapToSubscriptionViewModel().ToList()
            };
        }

        public static IEnumerable<SubscriptionViewModel> MapToSubscriptionViewModel(this IEnumerable<Subscription> subscriptions)
        {
            return subscriptions.Select(x => new SubscriptionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                QueryName = x.QueryName,
                Schedule = "TO DO"
            });
        }
    }
}