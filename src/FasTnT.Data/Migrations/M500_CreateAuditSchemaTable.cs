using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(500)]
    public class M500_CreateAuditSchemaTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("audit");
        }
    }
}
