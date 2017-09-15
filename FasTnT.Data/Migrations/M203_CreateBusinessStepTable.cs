using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(203)]
    public class M203_CreateBusinessStepTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_step").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey();
        }
    }
}
