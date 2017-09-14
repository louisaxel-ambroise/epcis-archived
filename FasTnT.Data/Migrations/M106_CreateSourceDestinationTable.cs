using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(106)]
    public class M106_CreateSourceDestinationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("source_destination").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("type").AsString(128).NotNullable()
                .WithColumn("source_dest_id").AsString(128).NotNullable()
                .WithColumn("Direction").AsInt16().NotNullable();

            Create.PrimaryKey("PK_EVENT_SOURCE_DESTINATION").OnTable("source_destination").WithSchema("epcis").Columns("event_id", "type");
        }
    }
}
