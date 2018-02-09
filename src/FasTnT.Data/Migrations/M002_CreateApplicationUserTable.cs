using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(002)]
    public class M002_CreateApplicationUserTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("application_user").InSchema("users")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(50).NotNullable().Unique()
                .WithColumn("created_on").AsDateTime().NotNullable()
                .WithColumn("last_logon").AsDateTime().Nullable()
                .WithColumn("mail").AsString(255).Nullable()
                .WithColumn("password_hash").AsString(1023).NotNullable()
                .WithColumn("password_salt").AsString(511).NotNullable()
                .WithColumn("is_active").AsBoolean().NotNullable()
                .WithColumn("user_type_id").AsInt16().NotNullable().ForeignKey("FK_USER_USERTYPE", "users", "user_type", "id")
                .WithColumn("preferred_language").AsString(10).Nullable();
        }
    }
}
