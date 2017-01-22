using Newtonsoft.Json.Linq;

namespace Epcis.Domain.Model.Epcis
{
    public class Epc
    {
        public virtual string Id { get; set; }
        public virtual JObject Ilmd { get; set; }
    }
}