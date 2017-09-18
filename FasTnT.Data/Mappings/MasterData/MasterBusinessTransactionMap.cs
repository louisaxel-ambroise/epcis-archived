using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.MasterData
{
    public class MasterBusinessTransactionMap : ClassMap<MasterBusinessTransaction>
    {
        public MasterBusinessTransactionMap()
        {
            Table("business_transaction");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
