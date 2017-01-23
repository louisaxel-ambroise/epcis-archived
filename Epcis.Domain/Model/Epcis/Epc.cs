using System.Xml.Linq;

namespace Epcis.Domain.Model.Epcis
{
    public class Epc
    {
        public virtual string Id { get; set; }
        public virtual Epc Parent { get; set; }
        public virtual bool IsActive { get; set; }
    }
}