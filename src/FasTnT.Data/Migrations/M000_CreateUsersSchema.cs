using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(000)]
    public class M000_CreateUsersSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Schema("users");
        }
    }
}
