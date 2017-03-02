using FluentMigrator;

namespace Epcis.Data.Migrations
{
    [Migration(003)]
    // ReSharper disable once InconsistentNaming
    public class M003_Create_VocabularyAttribute_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("VocabularyAttribute").InSchema("epcis")
                .WithColumn("VocabularyId").AsString(128).NotNullable().ForeignKey("FK_ATTRIBUTE_VOCABULARY", "epcis", "Vocabulary", "Id")
                .WithColumn("Type").AsString(128).NotNullable()
                .WithColumn("Value").AsString(255).NotNullable();

            Create.PrimaryKey("PK_VOCABULARY_ATTRIBUTE")
                .OnTable("VocabularyAttribute").WithSchema("epcis")
                .Columns("VocabularyId", "Type");
        }
    }
}