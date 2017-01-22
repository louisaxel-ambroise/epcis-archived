using System;
using System.Linq;
using Epcis.Domain.Model.CoreBusinessVocabulary;
using Epcis.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Epcis.Database.Repositories
{
    public class CoreBusinessEntityRepository : ICoreBusinessEntityRepository
    {
        private readonly ISession _session;

        public CoreBusinessEntityRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            _session = session;
        }

        public IQueryable<T> Query<T>() where T : CoreBusinessEntity
        {
            return _session.Query<T>();
        }

        public T LoadWithName<T>(string name) where T : CoreBusinessEntity
        {
            return _session.Query<T>().Single(x => x.Name == name);
        }

        public T Load<T>(int id) where T : CoreBusinessEntity
        {
            return _session.Load<T>(id);
        }

        public void Store<T>(T cbv) where T : CoreBusinessEntity
        {
            _session.Save(cbv);
        }
    }
}