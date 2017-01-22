using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1005)]
    // ReSharper disable once InconsistentNaming
    public class M1005_CreateEventToDestinationTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.EventToDestination)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("EventId").AsGuid().NotNullable().ForeignKey("FK_E2D_EVT", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Event, "Id")
                .WithColumn("DestinationId").AsInt32().NotNullable().ForeignKey("FK_E2D_SRC", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.SourceDest, "Id");
        }
    }
}