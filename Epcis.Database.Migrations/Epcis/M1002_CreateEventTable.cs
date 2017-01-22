using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1002)]
    // ReSharper disable once InconsistentNaming
    public class M1002_CreateEventTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.Event)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("Id").AsGuid().PrimaryKey("PK_EVENT")
                .WithColumn("Type").AsString(11).NotNullable()
                .WithColumn("EventTime").AsDateTime().NotNullable()
                .WithColumn("RecordTime").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("Action").AsInt16().NotNullable()
                .WithColumn("BizLocationId").AsInt32().ForeignKey("FK_EVENT_BIZLOCATION", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.BusinessLocation, "Id").NotNullable();
        }
    }
}