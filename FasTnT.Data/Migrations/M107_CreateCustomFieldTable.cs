using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(107)]
    public class M107_CreateCustomFieldTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("custom_field").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("namespace").AsString(128).NotNullable()
                .WithColumn("name").AsString(128).NotNullable()
                .WithColumn("type").AsInt16().NotNullable()
                .WithColumn("value").AsString(128).NotNullable();

            Create.PrimaryKey("PK_EVENT_CUSTOMFIELD").OnTable("custom_field").WithSchema("epcis").Columns("event_id", "namespace", "name");
        }
    }
}
