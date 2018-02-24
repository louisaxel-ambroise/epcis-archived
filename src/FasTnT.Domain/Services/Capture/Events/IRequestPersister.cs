using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.Capture.Events
{
    public interface IRequestPersister
    {
        void Persist(EpcisRequest request);
    }
}