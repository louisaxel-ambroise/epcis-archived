using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0006)]
    // ReSharper disable once InconsistentNaming
    public class M0006_CreateSourceDestTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.SourceDest)
                .InSchema(DatabaseConstants.Schemas.CoreBusinessVocabulary)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_SOURCEDEST")
                .WithColumn("Name").AsString(255).NotNullable().Unique("UQ_SRCDEST_NAME")
                .WithColumn("Type").AsString(255).Nullable();
        }
    }
}