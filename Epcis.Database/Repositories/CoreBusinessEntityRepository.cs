using System;
using System.Collections.Generic;
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
        private readonly IDictionary<string, CoreBusinessEntity> _cache; 

        public CoreBusinessEntityRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            _cache = new Dictionary<string, CoreBusinessEntity>();
        }

        public IQueryable<T> Query<T>() where T : CoreBusinessEntity
        {
            return _session.Query<T>();
        }

        public T LoadWithName<T>(string name) where T : CoreBusinessEntity
        {
            if (!_cache.ContainsKey(name))
                _cache[name] = _session.Query<T>().Single(x => x.Name == name);

            return _cache[name] as T;
        }

        public T Load<T>(int id) where T : CoreBusinessEntity
        {
            return _session.Load<T>(id);
        }

        public void Store<T>(T cbv) where T : CoreBusinessEntity
        {
            _session.Save(cbv);
            _cache[cbv.Name] = cbv;
        }
    }
}