using FasTnT.Domain.Model.Users;
using System;

namespace FasTnT.Web.Models.Users
{
    public class WebUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PreferredLanguage { get; set; }

        public static WebUser Create(User user)
        {
            return new WebUser
            {
                Id = user.Id,
                Name = user.Name,
                PreferredLanguage = user.PreferredLanguage
            };
        }
    }
}