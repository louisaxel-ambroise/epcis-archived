namespace FasTnT.Domain.Model.Events
{
    public enum EventType
    {
        ObjectEvent = 0,
        AggregationEvent = 1,
        TransformationEvent = 2,
        TransactionEvent = 3,
        QuantityEvent = 4
    }
}
