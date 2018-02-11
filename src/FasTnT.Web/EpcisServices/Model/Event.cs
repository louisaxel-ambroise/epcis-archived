using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract()]
    [KnownType(typeof(ObjectEvent))]
    public abstract class Event
    {
    }
}
