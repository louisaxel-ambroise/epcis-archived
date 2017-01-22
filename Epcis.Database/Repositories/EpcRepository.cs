using System;
using System.Linq;
using Epcis.Domain.Model.Epcis;
using Epcis.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Epcis.Database.Repositories
{
    public class EpcRepository : IEpcRepository
    {
        private readonly ISession _session;

        public EpcRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            _session = session;
        }

        public IQueryable<Epc> Query()
        {
            return _session.Query<Epc>();
        }

        public Epc Load(string id)
        {
            return _session.Load<Epc>(id);
        }

        public void Store(Epc epc)
        {
            _session.Save(epc);
        }
    }
}