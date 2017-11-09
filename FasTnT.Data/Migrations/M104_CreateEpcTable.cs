using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(104)]
    public class M104_CreateEpcTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("epc").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("epc").AsString(128).NotNullable()
                .WithColumn("type").AsInt16().NotNullable()
                .WithColumn("is_quantity").AsBoolean().NotNullable()
                .WithColumn("quantity").AsFloat().Nullable()
                .WithColumn("unit_of_measure").AsString(3).Nullable();

            Create.PrimaryKey("PK_EVENT_EPC").OnTable("epc").WithSchema("epcis").Columns("event_id", "epc");
        }
    }
}
