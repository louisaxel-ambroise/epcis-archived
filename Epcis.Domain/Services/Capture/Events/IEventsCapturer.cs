using Epcis.Domain.Model.Epcis;

namespace Epcis.Domain.Services.Capture.Events
{
    public interface IEventsCapturer
    {
        void CaptureEvents(BaseEvent[] events);
    }
}