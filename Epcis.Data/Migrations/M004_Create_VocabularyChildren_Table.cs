using FluentMigrator;

namespace Epcis.Data.Migrations
{
    [Migration(004)]
    // ReSharper disable once InconsistentNaming
    public class M004_Create_VocabularyChildren_Table : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("VocabularyChildren").InSchema("epcis")
                .WithColumn("ParentId").AsString(128).NotNullable().ForeignKey("FK_CHILDREN_VOCABULARY_PARENT", "epcis", "Vocabulary", "Id")
                .WithColumn("ChildrenId").AsString(128).NotNullable().ForeignKey("FK_CHILDREN_VOCABULARY_CHILDREN", "epcis", "Vocabulary", "Id");

            Create.PrimaryKey("PK_CHILDREN_ATTRIBUTE")
                .OnTable("VocabularyChildren").WithSchema("epcis")
                .Columns("ParentId", "ChildrenId");
        }
    }
}