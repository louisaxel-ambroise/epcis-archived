using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(003)]
    public class M003_CreateUserToUserTypeTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_to_user_type").InSchema("users")
                .WithColumn("user_id").AsGuid().NotNullable().ForeignKey("FK_USER_TO_TYPE", "users", "application_user", "id")
                .WithColumn("user_type_id").AsInt16().NotNullable().ForeignKey("FK_TYPE_TO_USER", "users", "user_type", "id");
        }
    }
}
