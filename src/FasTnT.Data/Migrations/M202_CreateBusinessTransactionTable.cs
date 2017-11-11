using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(202)]
    public class M202_CreateBusinessTransactionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_transaction").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey();
        }
    }
}
