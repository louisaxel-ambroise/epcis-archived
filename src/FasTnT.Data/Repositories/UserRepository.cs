using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System;

namespace FasTnT.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public User Load(Guid id)
        {
            return _session.Get<User>(id);
        }

        public IQueryable<User> Query()
        {
            return _session.Query<User>();
        }

        public User GetByUsername(string username)
        {
            return _session.Query<User>().Where(x => x.Name == username).SingleOrDefault();
        }
    }
}
