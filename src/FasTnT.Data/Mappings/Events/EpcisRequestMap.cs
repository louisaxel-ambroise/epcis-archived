using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class EpcisRequestMap : ClassMap<EpcisRequest>
    {
        public EpcisRequestMap()
        {
            Table("request");
            Schema("epcis");

            Id(x => x.Id).Column("id").GeneratedBy.GuidComb();

            Map(x => x.RecordTime).Column("capture_time");
            Map(x => x.DocumentTime).Column("document_time");
            References(x => x.User).Column("user_id");

            HasMany(x => x.Events).KeyColumn("request_id").Inverse().Cascade.Persist().LazyLoad();
        }
    }
}
