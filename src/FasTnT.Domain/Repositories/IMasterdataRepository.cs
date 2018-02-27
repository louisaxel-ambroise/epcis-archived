using FasTnT.Domain.Model.MasterData;
using System.Linq;

namespace FasTnT.Domain.Repositories
{
    public interface IMasterdataRepository
    {
        void Store(EpcisMasterdata masterdata);
        IQueryable<EpcisMasterdata> Query();
    }
}
