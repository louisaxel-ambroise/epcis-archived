using FasTnT.Domain.Log;
using NHibernate;
using FasTnT.Domain.Model.Log;

namespace FasTnT.Data.Log
{
    public class LogStore : ILogStore
    {
        private readonly ISessionFactory _sessionFactory;

        public LogStore(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Store(AuditLog log)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(log);

                session.Flush();
            }
        }
    }
}
