using Epcis.Model.Events;

namespace Epcis.Data.Storage
{
    public interface IEventStore
    {
        void Store(EpcisEvent epcisEvent);
    }
}