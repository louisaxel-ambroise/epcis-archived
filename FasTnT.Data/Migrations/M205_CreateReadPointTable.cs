using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(205)]
    public class M205_CreateReadPointTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("read_point").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey();
        }
    }
}
