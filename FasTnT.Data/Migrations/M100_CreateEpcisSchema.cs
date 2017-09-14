using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(100)]
    public class M100_CreateEpcisSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("epcis");
        }
    }
}
