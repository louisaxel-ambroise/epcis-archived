using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(102)]
    public class M102_CreateEventTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("event").InSchema("epcis")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("request_id").AsGuid().NotNullable()
                .WithColumn("user_id").AsGuid().Nullable()
                .WithColumn("capture_time").AsDateTime().NotNullable()
                .WithColumn("record_time").AsDateTime().NotNullable()
                .WithColumn("action").AsInt16().Nullable()
                .WithColumn("event_type").AsInt16().NotNullable().ForeignKey("FK_EVENT_EVENTTYPE", "epcis", "event_type", "id")
                .WithColumn("event_timezone_offset").AsInt16().NotNullable()
                .WithColumn("business_location").AsString(128).Nullable()
                .WithColumn("business_step").AsString(128).Nullable()
                .WithColumn("disposition").AsString(128).Nullable()
                .WithColumn("read_point").AsString(128).Nullable()
                .WithColumn("transformation_id").AsString(128).Nullable();
        }
    }
}
