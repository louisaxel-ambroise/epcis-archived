using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(200)]
    public class M200_CreateCbvSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("cbv");
        }
    }
}
