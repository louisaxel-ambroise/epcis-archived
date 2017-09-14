using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(107)]
    public class M107_CreateInstanceLotMasterDataTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("instance_lot_master_data").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("namespace").AsString(128).NotNullable()
                .WithColumn("name").AsString(128).NotNullable()
                .WithColumn("value").AsString(128).NotNullable();

            Create.PrimaryKey("PK_EVENT_ILMD").OnTable("instance_lot_master_data").WithSchema("epcis").Columns("event_id", "namespace", "name");
        }
    }
}
