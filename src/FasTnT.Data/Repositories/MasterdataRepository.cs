using System.Linq;
using FasTnT.Domain.Model.MasterData;
using FasTnT.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace FasTnT.Data.Repositories
{
    public class MasterdataRepository : IMasterdataRepository
    {
        private readonly ISession _session;

        public MasterdataRepository(ISession session)
        {
            _session = session;
        }

        public IQueryable<EpcisMasterdata> Query()
        {
            return _session.Query<EpcisMasterdata>();
        }

        public void Store(EpcisMasterdata masterdata)
        {
            _session.SaveOrUpdate(masterdata);
        }
    }
}
