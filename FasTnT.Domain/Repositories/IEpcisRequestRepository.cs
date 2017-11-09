using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Repositories
{
    public interface IEpcisRequestRepository
    {
        void Insert(EpcisRequest request);
    }
}
