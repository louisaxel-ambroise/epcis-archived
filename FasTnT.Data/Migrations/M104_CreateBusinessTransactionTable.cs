using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(104)]
    public class M104_CreateBusinessTransactionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_transaction").InSchema("epcis")
                .WithColumn("event_id").AsGuid().ForeignKey("FK_EPC_EVENT", "epcis", "event", "id")
                .WithColumn("transaction_type").AsString(128).NotNullable()
                .WithColumn("transaction_id").AsString(128).NotNullable();

            Create.PrimaryKey("PK_EVENT_BUSINESS_TRANSACTION").OnTable("business_transaction").WithSchema("epcis").Columns("event_id", "transaction_type", "transaction_id");
        }
    }
}
