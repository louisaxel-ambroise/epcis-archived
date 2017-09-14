using FasTnT.Domain.Model.Users;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Query();
        User GetByUsername(string username);
    }
}
