using Epcis.Domain.Model.Epcis;

namespace Epcis.Domain.Services.Mapping
{
    public interface IEventMapper
    {
        BaseEvent MapEvent(EventParameters parameters);
        ObjectEvent MapObjectEvent(EventParameters parameters);
        TransactionEvent MapTransactionEvent(EventParameters parameters);
        AggregationEvent MapAggregationEvent(EventParameters parameters);
    }
}