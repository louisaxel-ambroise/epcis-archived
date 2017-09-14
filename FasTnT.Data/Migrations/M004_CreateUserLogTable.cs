using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(004)]
    public class M004_CreateUserLogTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_log").InSchema("users")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("user_id").AsGuid().NotNullable().ForeignKey("FK_LOG_TO_USER", "users", "application_user", "id")
                .WithColumn("log_type").AsInt32().NotNullable()
                .WithColumn("payload").AsCustom("jsonb").Nullable()
                .WithColumn("timestamp").AsDateTime().NotNullable();
        }
    }
}
