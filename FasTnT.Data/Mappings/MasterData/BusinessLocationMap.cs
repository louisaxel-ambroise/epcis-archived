using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class BusinessLocationMap : ClassMap<BusinessLocation>
    {
        public BusinessLocationMap()
        {
            Table("business_location");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
