using Epcis.Domain.Model.Epcis;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings
{
    public class EpcMap : ClassMap<Epc>
    {
        public EpcMap()
        {
            Schema(DatabaseConstants.Schemas.Epcis);
            Table(DatabaseConstants.Tables.Epc);

            Id(x => x.Id).Column("Id");
        }
    }
}