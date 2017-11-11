using System.ServiceModel.Channels;
using System.Xml.Linq;

namespace FasTnT.Web.EpcisServices.Model
{
    public static class MessageResponse
    {
        public static Message CreatePollResponse(XElement results)
        {
            return Message.CreateMessage(MessageVersion.Soap11, "http://schemas.xmlsoap.org/wsdl/EpcisQuery/pollResponse", results);
        }
    }
}