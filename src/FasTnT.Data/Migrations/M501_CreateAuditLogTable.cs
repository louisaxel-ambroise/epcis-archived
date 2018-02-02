using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(501)]
    public class M501_CreateAuditLogTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("log").InSchema("audit")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("timestamp").AsDateTime().NotNullable()
                .WithColumn("user_id").AsGuid().ForeignKey("fk_auditlog_user", "users", "application_user", "id").NotNullable()
                .WithColumn("action").AsString().NotNullable()
                .WithColumn("description").AsString(254).NotNullable()
                .WithColumn("type").AsString(25).NotNullable();
        }
    }
}
