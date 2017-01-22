using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0003)]
    // ReSharper disable once InconsistentNaming
    public class M0003_CreateBusinessStepTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.BusinessStep)
                .InSchema(DatabaseConstants.Schemas.CoreBusinessVocabulary)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_BIZSTEP")
                .WithColumn("Name").AsString(255).NotNullable().Unique("UQ_BIZSTEP_NAME")
                .WithColumn("Type").AsString(255).Nullable();
        }
    }
}