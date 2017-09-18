using System.Xml.Linq;
using FasTnT.Domain.Model.Events;

namespace FasTnT.Domain.Services.Formatting
{
    public interface IEventFormatter
    {
        XElement Format(EpcisEvent @event);
    }
}
