using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(400)]
    public class M400_CreateSubscriptionsSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("subscriptions");
        }
    }
}
