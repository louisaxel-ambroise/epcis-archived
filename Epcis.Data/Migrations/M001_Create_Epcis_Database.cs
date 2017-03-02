using FluentMigrator;

namespace Epcis.Data.Migrations
{
    [Migration(001)]
    // ReSharper disable once InconsistentNaming
    public class M001_CreateEpcisDatabase : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.EmbeddedScript(@"M001_CreateEpcisDatabase.sql");
        }
    }
}