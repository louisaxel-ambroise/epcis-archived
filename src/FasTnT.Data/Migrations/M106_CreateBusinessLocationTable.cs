using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(106)]
    public class M106_CreateBusinessLocationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_location").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("location_id").AsString(128).NotNullable();

            Create.PrimaryKey("PK_EVENT_BUSINESS_LOCATION").OnTable("business_location").WithSchema("epcis").Columns("event_id", "location_id");
        }
    }
}
