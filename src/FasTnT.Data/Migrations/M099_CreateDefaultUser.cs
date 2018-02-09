using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Utils;
using FluentMigrator;
using System;

namespace FasTnT.Data.Migrations
{
    [Migration(099)]
    public class M099_CreateDefaultUser : Migration
    {
        public override void Down()
        {
            Delete.FromTable("application_user").InSchema("users")
                .Row(new { id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70") })
                .Row(new { id = Guid.Parse("f0181a45-9bd5-45b4-859f-20155a0bfeef") });
        }

        public override void Up()
        {
            Insert.IntoTable("application_user").InSchema("users")
                .Row(new
                {
                    id = Guid.Parse("a68281f1-e2f3-4d12-ad08-d7bc541a9c70"),
                    name = "Admin",
                    created_on = SystemContext.Clock.Now,
                    mail = "ambroise.la@gmail.com",
                    password_hash = "$2a$05$Dl/.8P8.L0T/ZfpGiThkGOghtbUS/oZsvisW71zg0lXKnBgENR4gq", // p@ssw0rd
                    password_salt = "$2a$05$Dl/.8P8.L0T/ZfpGiThkGO",
                    is_active = true,
                    user_type_id = UserType.DashboardUser.Id,
                    preferred_language = "en-GB"
                })
                .Row(new
                {
                    id = Guid.Parse("f0181a45-9bd5-45b4-859f-20155a0bfeef"),
                    name = "APIUser",
                    created_on = SystemContext.Clock.Now,
                    password_hash = "$2a$10$XXVrbafgMCAz8WIq10M46eT7h7jYo5pRmLYQ/PqdCFzvfc0vW7v/6", // ApiP@ssw0rd
                    password_salt = "$2a$10$XXVrbafgMCAz8WIq10M46e",
                    is_active = true,
                    user_type_id = UserType.ApiUser.Id,
                    preferred_language = "en-GB"
                });
        }
    }
}
