using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(201)]
    public class M201_CreateBusinessLocationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_location").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey();
        }
    }
}
