using System.Collections.Generic;
using Epcis.Model.Events;

namespace Epcis.Services.Capture.Parsing
{
    public interface IEventParser<in T>
    {
        IEnumerable<EpcisEvent> Parse(T input);
    }
}