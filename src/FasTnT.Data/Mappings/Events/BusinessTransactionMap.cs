using FasTnT.Domain.Model.Events;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class BusinessTransactionMap : ClassMap<BusinessTransaction>
    {
        public BusinessTransactionMap()
        {
            Table("business_transaction");
            Schema("epcis");

            CompositeId()
                .KeyReference(x => x.Event, "event_id")
                .KeyProperty(x => x.Type, "transaction_type")
                .KeyProperty(x => x.Id, "transaction_id");
        }
    }
}
