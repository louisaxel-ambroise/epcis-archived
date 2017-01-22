using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0005)]
    // ReSharper disable once InconsistentNaming
    public class M0005_CreateDispositionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.Disposition)
                .InSchema(DatabaseConstants.Schemas.CoreBusinessVocabulary)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_DISPOSITION")
                .WithColumn("Name").AsString(255).NotNullable().Unique("UQ_DISPOSITION_NAME")
                .WithColumn("Type").AsString(255).Nullable();
        }
    }
}