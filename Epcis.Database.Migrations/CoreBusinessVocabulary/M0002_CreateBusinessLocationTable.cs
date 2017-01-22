using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0002)]
    // ReSharper disable once InconsistentNaming
    public class M0002_CreateBusinessLocationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.BusinessLocation)
                .InSchema(DatabaseConstants.Schemas.CoreBusinessVocabulary)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_BIZLOCATION")
                .WithColumn("Name").AsString(255).NotNullable().Unique("UQ_BIZLOCATION_NAME")
                .WithColumn("Type").AsString(255).Nullable();
        }
    }
}