using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(402)]
    public class M402_CreateSubscriptionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("subscription").InSchema("subscriptions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("subscription_name").AsString(128).NotNullable()
                .WithColumn("user_id").AsGuid().ForeignKey("fk_subscription_user", "users", "application_user", "id").NotNullable()
                .WithColumn("trigger").AsString(1023).Nullable()
                .WithColumn("controls_initial_record_time").AsDateTime().NotNullable()
                .WithColumn("controls_report_if_empty").AsBoolean().NotNullable()
                .WithColumn("destination_url").AsString(128).NotNullable()
                .WithColumn("query_name").AsString(128).NotNullable()
                .WithColumn("schedule_id").AsGuid().ForeignKey("fk_subscription_schedule", "subscriptions", "schedule", "id");

            Create.UniqueConstraint("uq_subscription_name_user").OnTable("subscription").WithSchema("subscriptions").Columns("id", "user_id");
        }
    }
}
