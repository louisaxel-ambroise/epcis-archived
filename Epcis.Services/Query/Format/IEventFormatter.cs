using Epcis.Model;

namespace Epcis.Services.Query.Format
{
    public interface IEventFormatter<out T>
    {
        bool CanFormat(EpcisEvent epcisEvent);
        T Format(EpcisEvent epcisEvent);
    }
}