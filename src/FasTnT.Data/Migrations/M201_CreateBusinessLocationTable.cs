using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(201)]
    public class M201_CreateBusinessLocationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("business_location").InSchema("cbv")
                .WithColumn("id").AsString(128).PrimaryKey()
                .WithColumn("created_on").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("last_update").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("name").AsString(128).Nullable()
                .WithColumn("address").AsString(128).Nullable()
                .WithColumn("country").AsString(64).Nullable()
                .WithColumn("latitude").AsFloat().Nullable()
                .WithColumn("longitude").AsFloat().Nullable();
        }
    }
}
