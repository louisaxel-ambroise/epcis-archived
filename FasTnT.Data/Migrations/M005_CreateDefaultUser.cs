using FasTnT.Domain.Model.Users;
using FluentMigrator;
using System;

namespace FasTnT.Data.Migrations
{
    [Migration(005)]
    public class M005_CreateDefaultUser : Migration
    {
        public override void Down()
        {
            Delete.FromTable("user_to_user_type").InSchema("users")
                .Row(new { user_id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70"), user_type_id = UserType.DashboardUser.Id });

            Delete.FromTable("application_user").InSchema("users")
                .Row(new { id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70") });
        }

        public override void Up()
        {
            Insert.IntoTable("application_user").InSchema("users")
                .Row(new
                {
                    id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70"),
                    name = "Admin",
                    created_on = new DateTime(2017,09,13),
                    mail = "ambroise.la@gmail.com",
                    password_hash = "$2a$05$Dl/.8P8.L0T/ZfpGiThkGOghtbUS/oZsvisW71zg0lXKnBgENR4gq",
                    password_salt = "$2a$05$Dl/.8P8.L0T/ZfpGiThkGO",
                    is_active = true,
                    preferred_language = "en-GB"
                });

            Insert.IntoTable("user_to_user_type").InSchema("users")
                .Row(new { user_id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70"), user_type_id = UserType.DashboardUser.Id });
        }
    }
}
