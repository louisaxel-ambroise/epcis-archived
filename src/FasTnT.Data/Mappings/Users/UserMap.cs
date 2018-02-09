using FasTnT.Domain.Model.Users;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Users
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("application_user");
            Schema("users");
            DynamicUpdate();

            Id(x => x.Id).Column("id");
            Map(x => x.Name).Column("name").Unique().Not.Nullable();
            Map(x => x.Mail).Column("mail").Unique().Nullable();
            Map(x => x.PasswordHash).Column("password_hash").Not.Nullable();
            Map(x => x.PasswordSalt).Column("password_salt").Not.Nullable();
            Map(x => x.LastLogOn).Column("last_logon").Nullable();
            Map(x => x.IsActive).Column("is_active").Not.Nullable();
            Map(x => x.CreatedOn).Column("created_on").Not.Nullable();
            Map(x => x.PreferredLanguage).Column("preferred_language").Nullable();

            References(x => x.Role).Column("user_type_id");
            HasMany(x => x.Logs).KeyColumn("user_id").Cascade.AllDeleteOrphan();
        }
    }
}
