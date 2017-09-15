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
            Map(x => x.CreatedOn).Column("created_on");
            Map(x => x.LastUpdated).Column("last_update");
            Map(x => x.Name).Column("name");
            Map(x => x.Address).Column("address");
            Map(x => x.Street).Column("street");
            Map(x => x.Country).Column("country");
            Map(x => x.Latitude).Column("gps_latitude");
            Map(x => x.Longitude).Column("gps_longitude");
        }
    }
}
