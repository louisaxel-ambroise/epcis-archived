using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class TransformationEventMap : SubclassMap<TransformationEvent>
    {
        public TransformationEventMap()
        {
            DiscriminatorValue("trf");
        }
    }
}