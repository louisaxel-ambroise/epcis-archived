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
            Map(x => x.Timestamp).Column("timestamp").Not.Nullable();
            Map(x => x.Type).Column("type").Nullable();
            Map(x => x.Action).Column("action").Not.Nullable();
            Map(x => x.Description).Column("description").Not.Nullable();
            Map(x => x.ExecutionTimeMs).Column("execution_time").Not.Nullable();

            References(x => x.User).Column("user_id").Nullable();
        }
    }
}
