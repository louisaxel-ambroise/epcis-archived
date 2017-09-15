using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class BusinessTransactionMap : ClassMap<BusinessTransaction>
    {
        public BusinessTransactionMap()
        {
            Table("business_transaction");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
