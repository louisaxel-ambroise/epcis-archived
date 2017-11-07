using FasTnT.Domain.Model.Dashboard;

namespace FasTnT.Domain.Services.Dashboard.Users
{
    public interface IUserService
    {
        UserDetail GetDetails(string userName);
        void SetPassword(string userName, string previous, string updated);
    }
}