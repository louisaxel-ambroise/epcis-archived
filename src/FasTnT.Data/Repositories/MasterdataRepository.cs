using FasTnT.Domain.Model.MasterData;
using FasTnT.Domain.Repositories;
using NHibernate;

namespace FasTnT.Data.Repositories
{
    public class MasterdataRepository : IMasterdataRepository
    {
        private readonly ISession _session;

        public MasterdataRepository(ISession session)
        {
            _session = session;
        }

        public void Store(EpcisMasterdata masterdata)
        {
            _session.Save(masterdata);
        }
    }
}
