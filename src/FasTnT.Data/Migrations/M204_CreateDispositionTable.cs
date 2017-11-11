using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(204)]
    public class M204_CreateDispositionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("disposition").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey();
        }
    }
}
