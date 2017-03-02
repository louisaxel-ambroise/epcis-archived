using Epcis.Model.Events;

namespace Epcis.Services.Query.EventFormatters
{
    public interface IEventFormatter<out T>
    {
        bool CanFormat(EpcisEvent epcisEvent);
        T Format(EpcisEvent epcisEvent);
    }
}