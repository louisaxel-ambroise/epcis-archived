using FasTnT.Domain.Model.Events;
using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    
    [Migration(300)]
    public class M300_InsertDetaultData : Migration
    {
        public override void Down()
        {
            Delete.FromTable("event_type").InSchema("epcis")
                .AllRows();
        }

        public override void Up()
        {
            Insert.IntoTable("event_type").InSchema("epcis")
                .Row(new { id = (int)EventType.Object, name = "OBJECT", is_deprecated = false })
                .Row(new { id = (int)EventType.Aggregation, name = "AGGREGATION", is_deprecated = false })
                .Row(new { id = (int)EventType.Transaction, name = "TRANSACTION", is_deprecated = false })
                .Row(new { id = (int)EventType.Transformation, name = "TRANSFORMATION", is_deprecated = false })
                .Row(new { id = (int)EventType.Quantity, name = "QUANTITY", is_deprecated = true });
        }
    }
}
