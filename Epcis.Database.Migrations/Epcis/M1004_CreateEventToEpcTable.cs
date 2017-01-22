using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1004)]
    // ReSharper disable once InconsistentNaming
    public class M1004_CreateEventToEpcTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.EventToEpc)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("EventId").AsGuid().NotNullable().ForeignKey("FK_E2E_EVT", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Event, "Id")
                .WithColumn("EpcId").AsString(155).NotNullable().ForeignKey("FK_E2E_EPC", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Epc, "Id");
        }
    }
}