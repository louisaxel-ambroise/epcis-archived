using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(406)]
    public class M406_CreatePendingEventTrigger : Migration
    {
        public override void Down()
        {
            Execute.EmbeddedScript(@"M406_CreatePendingEventTrigger.Down.sql");
        }

        public override void Up()
        {
            Execute.EmbeddedScript(@"M406_CreatePendingEventTrigger.Up.sql");
        }
    }
}
