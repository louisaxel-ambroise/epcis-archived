using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(401)]
    public class M401_CreateSubscriptionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("subscription").InSchema("subscriptions")
                .WithColumn("id").AsString(128).PrimaryKey()
                .WithColumn("trigger").AsString(1023).Nullable()
                .WithColumn("schedule").AsString(128).Nullable()
                .WithColumn("initial_report_time").AsDateTime().NotNullable()
                .WithColumn("report_if_empty").AsBoolean().NotNullable()
                .WithColumn("destination_url").AsString(128).NotNullable()
                .WithColumn("destination_headers").AsString(int.MaxValue).Nullable()
                .WithColumn("query_name").AsString(128).NotNullable()
                .WithColumn("query_parameters").AsString(int.MaxValue).Nullable();
        }
    }
}
