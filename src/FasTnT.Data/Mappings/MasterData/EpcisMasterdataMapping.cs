using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.MasterData
{
    public class EpcisMasterdataMapping : ClassMap<EpcisMasterdata>
    {
        public EpcisMasterdataMapping()
        {
            Schema("cbv");
            Table("masterdata");

            CompositeId()
                .KeyProperty(x => x.Id, "id")
                .KeyProperty(x => x.Type, "type");

            Map(x => x.CreatedOn).Column("created_on").Not.Nullable().Not.Update();
            Map(x => x.LastUpdatedOn).Column("last_update").Not.Nullable();

            HasMany(x => x.Attributes).KeyColumns.Add("masterdata_id", "masterdata_type").Inverse().Cascade.Persist().LazyLoad();
        }
    }
}
