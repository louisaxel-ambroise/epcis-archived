using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(202)]
    public class M202_CreateMasterdataAttributeTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("attribute").InSchema("cbv")
                .WithColumn("masterdata_id").AsString(128).NotNullable()
                .WithColumn("masterdata_type").AsString(128).NotNullable()
                .WithColumn("id").AsString(128).NotNullable()
                .WithColumn("value").AsString(128).NotNullable();

            Create.PrimaryKey("pk_cbv_masterdata_attribute").OnTable("attribute").WithSchema("cbv").Columns("masterdata_id", "masterdata_type", "id");
            Create.ForeignKey("fl_cbv_attribute_to_masterdata")
                .FromTable("attribute").InSchema("cbv").ForeignColumns("masterdata_id", "masterdata_type")
                .ToTable("masterdata").InSchema("cbv").PrimaryColumns("id", "type");
        }
    }
}
