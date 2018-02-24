using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(201)]
    public class M201_CreateMasterdataTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("masterdata").InSchema("cbv")
                .WithColumn("id").AsString(128).NotNullable()
                .WithColumn("type").AsString(128).NotNullable()
                .WithColumn("created_on").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("last_update").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

            Create.PrimaryKey("pk_cbv_masterdata").OnTable("masterdata").WithSchema("cbv").Columns("id", "type");
        }
    }
}
