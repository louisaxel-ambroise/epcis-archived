using FasTnT.Data.Log;
using FasTnT.Domain.Model.Log;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Log
{
    public class AuditLogMap : ClassMap<AuditLog>
    {
        public AuditLogMap()
        {
            Table("log");
            Schema("audit");

            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Timestamp).Column("timestamp");
            Map(x => x.Type).Column("type");
            Map(x => x.Action).Column("action");
            Map(x => x.Description).Column("description");

            References(x => x.User).Column("user_id");
        }
    }
}
