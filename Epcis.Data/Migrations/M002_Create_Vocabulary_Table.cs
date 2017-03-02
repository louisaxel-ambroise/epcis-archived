using FluentMigrator;

namespace Epcis.Data.Migrations
{
    [Migration(002)]
    // ReSharper disable once InconsistentNaming
    public class M002_Create_Vocabulary_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Vocabulary").InSchema("epcis")
                .WithColumn("Id").AsString(128).PrimaryKey("PK_VOCABULARY")
                .WithColumn("Type").AsString(128).NotNullable()
                .WithColumn("IsActive").AsBoolean();
        }
    }
}