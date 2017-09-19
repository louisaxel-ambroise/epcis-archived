using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.EventCapture
{
    public interface IEventPersister
    {
        void Persist(EpcisEvent @event);
    }
}