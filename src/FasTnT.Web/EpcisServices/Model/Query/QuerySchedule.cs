using System.Runtime.Serialization;

namespace FasTnT.Web.EpcisServices
{
    [DataContract(Name = "subscribe", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public class QuerySchedule
    {
        [DataMember(Name = "second")]
        public string Second { get; set; }

        [DataMember(Name = "minute")]
        public string Minute { get; set; }

        [DataMember(Name = "hour")]
        public string Hour { get; set; }

        [DataMember(Name = "dayOfMonth")]
        public string DayOfMonth { get; set; }

        [DataMember(Name = "month")]
        public string Month { get; set; }

        [DataMember(Name = "dayOfWeek")]
        public string DayOfWeek { get; set; }
    }
}
