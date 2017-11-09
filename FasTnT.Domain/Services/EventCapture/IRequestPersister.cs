using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.EventCapture
{
    public interface IRequestPersister
    {
        void Persist(EpcisRequest request);
    }
}