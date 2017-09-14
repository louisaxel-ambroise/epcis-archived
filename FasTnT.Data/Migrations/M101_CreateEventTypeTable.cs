using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(101)]
    public class M101_CreateEventTypeTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("event_type").InSchema("epcis")
                .WithColumn("id").AsInt16().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable().Unique()
                .WithColumn("is_deprecated").AsBoolean().WithDefaultValue(false);
        }
    }
}
