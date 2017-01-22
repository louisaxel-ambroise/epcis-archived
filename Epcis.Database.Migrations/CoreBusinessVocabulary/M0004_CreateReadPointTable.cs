using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0004)]
    // ReSharper disable once InconsistentNaming
    public class M0004_CreateReadPointTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.ReadPoint)
                .InSchema(DatabaseConstants.Schemas.CoreBusinessVocabulary)
                .WithColumn("Id").AsInt32().PrimaryKey("PK_READPOINT")
                .WithColumn("Name").AsString(255).NotNullable().Unique("UQ_READPOINT_NAME")
                .WithColumn("Type").AsString(255).Nullable();
        }
    }
}