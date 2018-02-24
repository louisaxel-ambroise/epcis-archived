using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace FasTnT.Web.EpcisServices
{
    [MessageContract]
    public class QueryResults
    {
        [MessageBodyMember(Name = "queryName", Order = 0)]
        public string QueryName { get; set; }

        [MessageBodyMember(Name = "subscriptionID", Order = 1)]
        public string SubscriptionId { get; set; }

        [MessageBodyMember(Name = "eventList", Order = 2)]
        public EventList EventList { get; set; }
    }

    public class EventList : ICustomSerializable
    {
        public IEnumerable<XElement> Elements { get; set; }

        public EventList() { }

        public void InitializeFrom(Stream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(Stream stream)
        {
            var ser = Elements.Select(x => x.ToString(SaveOptions.DisableFormatting));
            var data = Encoding.UTF8.GetBytes(string.Join("", ser));

            stream.Write(data, 0, data.Length);
        }
    }
}
