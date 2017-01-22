using Epcis.Domain.Model.CoreBusinessVocabulary;
using FluentNHibernate.Mapping;

namespace Epcis.Database.Mappings.CoreBusinessEntities
{
    public class SourceDestinationMap : ClassMap<SourceDestination>
    {
        public SourceDestinationMap()
        {
            Schema(DatabaseConstants.Schemas.CoreBusinessVocabulary);
            Table(DatabaseConstants.Tables.SourceDest);

            Id(x => x.Id).Column("Id").GeneratedBy.Native();
            Map(x => x.Name).Column("Name").Not.Nullable();
            Map(x => x.Type).Column("Type").Nullable();
        }
    }
}