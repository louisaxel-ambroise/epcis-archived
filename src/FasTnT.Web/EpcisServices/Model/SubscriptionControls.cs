using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "SubscriptionControls", Namespace = "")]
    public class SubscriptionControls
    {
        [DataMember(Name = "reportIfEmpty")]
        public bool ReportIfEmpty { get; set; }
    }
}
