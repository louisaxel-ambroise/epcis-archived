using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1002)]
    // ReSharper disable once InconsistentNaming
    public class M1002_CreateEpcTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.Epc)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("Id").AsString(155).PrimaryKey("PK_EPC")
                .WithColumn("Active").AsBoolean().WithDefaultValue(true)
                .WithColumn("ParentId").AsString(155).ForeignKey("FK_EPC_EPC", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Epc, "Id").Nullable()
                .WithColumn("Ilmd").AsXml().Nullable();
        }
    }
}