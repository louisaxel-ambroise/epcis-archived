using FasTnT.Domain.Model.Dashboard;
using System;

namespace FasTnT.Domain.Services.Dashboard.Users
{
    public interface IUserService
    {
        UserDetail GetDetails(string userName);
        void SetPassword(Guid userId, string previous, string updated);
        void SetPreferredLanguage(Guid userId, string language);
    }
}