using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class BusinessStepMap : ClassMap<BusinessStep>
    {
        public BusinessStepMap()
        {
            Table("business_step");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
