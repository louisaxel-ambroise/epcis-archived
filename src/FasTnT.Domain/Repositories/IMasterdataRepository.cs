using FasTnT.Domain.Model.MasterData;

namespace FasTnT.Domain.Repositories
{
    public interface IMasterdataRepository
    {
        void Store(EpcisMasterdata masterdata);
    }
}
