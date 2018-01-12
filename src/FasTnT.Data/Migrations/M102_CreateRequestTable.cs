using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(102)]
    public class M102_CreateRequestTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("request").InSchema("epcis")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("document_time").AsDateTime().NotNullable()
                .WithColumn("record_time").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("user_id").AsGuid().Nullable();
        }
    }
}
