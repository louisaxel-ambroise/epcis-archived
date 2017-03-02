using FluentMigrator;

namespace Epcis.Data.Migrations
{
    [Migration(005)]
    // ReSharper disable once InconsistentNaming
    public class M005_Create_Quartz_Tables : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("M005_Create_Quartz_Tables.sql");
        }
    }
}