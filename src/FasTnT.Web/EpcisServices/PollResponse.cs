using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "pollResponse")]
    public class PollResponse
    {
        [DataMember]
        public string Text { get; set; }
    }
}
