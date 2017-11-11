using FasTnT.Domain.Model.Users;
using System;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface IUserRepository
    {
        User Load(Guid id);
        IQueryable<User> Query();
        User GetByUsername(string username);
    }
}
