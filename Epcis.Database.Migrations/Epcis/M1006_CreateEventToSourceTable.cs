using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1006)]
    // ReSharper disable once InconsistentNaming
    public class M1006_CreateEventToSourceTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.EventToSource)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("EventId").AsGuid().NotNullable().ForeignKey("FK_E2S_EVT", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Event, "Id")
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_E2S_SRC", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.SourceDest, "Id");
        }
    }
}