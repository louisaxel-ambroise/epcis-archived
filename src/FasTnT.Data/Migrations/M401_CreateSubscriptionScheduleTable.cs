using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(401)]
    public class M401_CreateSubscriptionScheduleTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("schedule").InSchema("subscriptions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("seconds").AsString(255)
                .WithColumn("minutes").AsString(255)
                .WithColumn("hours").AsString(255)
                .WithColumn("day_of_month").AsString(255)
                .WithColumn("month").AsString(255)
                .WithColumn("day_of_week").AsString(255);
        }
    }
}
