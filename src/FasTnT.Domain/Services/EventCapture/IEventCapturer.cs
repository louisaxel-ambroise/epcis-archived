using FasTnT.Domain.Model.Capture;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.EventCapture
{
    public interface IEventCapturer
    {
        CaptureResponse Capture(XDocument xmlDocument);
    }
}