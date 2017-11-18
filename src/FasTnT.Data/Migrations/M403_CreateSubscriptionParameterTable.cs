using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(403)]
    public class M403_CreateSubscriptionParameterTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("parameter").InSchema("subscriptions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(1023).Nullable()
                .WithColumn("subscription_id").AsString(128).NotNullable().ForeignKey("fk_subscription_parameter", "subscriptions", "subscription", "id");
        }
    }
}
