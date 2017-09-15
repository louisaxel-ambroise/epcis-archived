using FasTnT.Domain.Model.MasterData;
using FluentNHibernate.Mapping;

namespace FasTnT.Data.Mappings.Events
{
    public class ReadPointMap : ClassMap<ReadPoint>
    {
        public ReadPointMap()
        {
            Table("read_point");
            Schema("cbv");

            Id(x => x.Id).Column("id");
        }
    }
}
