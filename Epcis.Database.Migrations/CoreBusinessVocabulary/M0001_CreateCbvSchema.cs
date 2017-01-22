using FluentMigrator;

namespace Epcis.Database.Migrations.CoreBusinessVocabulary
{
    [Migration(0001)]
    // ReSharper disable once InconsistentNaming
    public class M0001_CreateCbvSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema(DatabaseConstants.Schemas.CoreBusinessVocabulary);
        }
    }
}