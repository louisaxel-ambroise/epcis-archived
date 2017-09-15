using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class DispositionMap : ClassMap<Disposition>
    {
        public DispositionMap()
        {
            Table("disposition");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
