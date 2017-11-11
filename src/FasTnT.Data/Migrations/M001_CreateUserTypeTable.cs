using FluentMigrator;
using FasTnT.Domain.Model.Users;

namespace FasTnT.Data.Migrations
{
    [Migration(001)]
    public class M001_CreateUserTypeTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_type").InSchema("users")
                .WithColumn("id").AsInt16().PrimaryKey()
                .WithColumn("name").AsString(50).NotNullable();

            // Insert default data
            Insert.IntoTable("user_type").InSchema("users")
                .Row(new { id = UserType.ApiUser.Id, name = UserType.ApiUser.Name })
                .Row(new { id = UserType.DashboardUser.Id, name = UserType.DashboardUser.Name });
        }
    }
}
