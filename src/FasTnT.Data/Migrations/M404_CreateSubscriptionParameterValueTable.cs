using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    [Migration(404)]
    public class M404_CreateSubscriptionParameterValueTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("parameter_value").InSchema("subscriptions")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("parameter_id").AsGuid().NotNullable().ForeignKey("fk_value_parameter", "subscriptions", "parameter", "id")
                .WithColumn("value").AsString(255).NotNullable();
        }
    }
}
