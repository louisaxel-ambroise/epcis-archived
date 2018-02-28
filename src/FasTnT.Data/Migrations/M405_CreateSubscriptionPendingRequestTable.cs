using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(405)]
    public class M405_CreateSubscriptionPendingRequestTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("pendingrequest").InSchema("subscriptions")
                .WithColumn("subscription_id").AsGuid().NotNullable().ForeignKey("fk_subscription_pending", "subscriptions", "subscription", "id")
                .WithColumn("request_id").AsGuid().NotNullable().ForeignKey("fh_pending_request", "epcis", "request", "id");
        }
    }
}
