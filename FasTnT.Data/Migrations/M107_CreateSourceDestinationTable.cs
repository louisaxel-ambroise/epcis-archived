using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(107)]
    public class M107_CreateSourceDestinationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("source_destination").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("type").AsString(128).NotNullable()
                .WithColumn("source_dest_id").AsString(128).NotNullable()
                .WithColumn("direction").AsInt16().NotNullable();

            Create.PrimaryKey("PK_EVENT_SOURCE_DESTINATION").OnTable("source_destination").WithSchema("epcis").Columns("event_id", "type");
        }
    }
}
