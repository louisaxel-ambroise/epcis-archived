using FasTnT.Domain.Model.Users;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings
{
    public class UserLogMap : ClassMap<UserLog>
    {
        public UserLogMap()
        {
            Table("user_log");
            Schema("users");

            Id(x => x.Id).Column("id");
            Map(x => x.Timestamp).Column("timestamp").Not.Nullable();
            Map(x => x.Type).Column("log_type").CustomType<UserLogType>();
            References(x => x.AppliesTo).Column("user_id");
        }
    }
}
