using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Epcis.Database.Mappings
{
    public class EpcMap : ClassMap<Epc>
    {
        public EpcMap()
        {
            Schema(DatabaseConstants.Schemas.Epcis);
            Table(DatabaseConstants.Tables.Epc);

            Id(x => x.Id).Column("Id");
            Map(x => x.IsActive).Column("Active").Not.Nullable();
            References(x => x.Parent).Column("ParentId").Nullable();
        }
    }
}