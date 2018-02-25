using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.MasterData
{
    public class MasterdataAttributeMapping : ClassMap<MasterdataAttribute>
    {
        public MasterdataAttributeMapping()
        {
            Schema("cbv");
            Table("attribute");

            CompositeId()
                .KeyProperty(x => x.Id, "id")
                .KeyReference(x => x.MasterData, "masterdata_id", "masterdata_type");

            Map(x => x.Value).Column("value").Not.Nullable();
        }
    }
}
