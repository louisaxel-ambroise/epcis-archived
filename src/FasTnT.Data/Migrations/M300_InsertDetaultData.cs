using FasTnT.Domain.Model.Events;
using FluentMigrator;

namespace FasTnT.Data.Migrations
{
    
    [Migration(300)]
    public class M300_InsertDetaultData : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable("event_type").InSchema("epcis")
                .Row(new { id = (int)EventType.ObjectEvent, name = "OBJECT", is_deprecated = false })
                .Row(new { id = (int)EventType.AggregationEvent, name = "AGGREGATION", is_deprecated = false })
                .Row(new { id = (int)EventType.TransactionEvent, name = "TRANSACTION", is_deprecated = false })
                .Row(new { id = (int)EventType.TransformationEvent, name = "TRANSFORMATION", is_deprecated = false })
                .Row(new { id = (int)EventType.QuantityEvent, name = "QUANTITY", is_deprecated = true });
        }
    }
}
