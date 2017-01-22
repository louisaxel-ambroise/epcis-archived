using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1003)]
    // ReSharper disable once InconsistentNaming
    public class M1003_CreateEpcTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.Epc)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("Id").AsString(155).PrimaryKey("PK_EPC");
        }
    }
}