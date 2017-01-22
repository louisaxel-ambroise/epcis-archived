using FluentMigrator;

namespace Epcis.Database.Migrations.Epcis
{
    [Migration(1003)]
    // ReSharper disable once InconsistentNaming
    public class M1003_CreateEventTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(DatabaseConstants.Tables.Event)
                .InSchema(DatabaseConstants.Schemas.Epcis)
                .WithColumn("Id").AsGuid().PrimaryKey("PK_EVENT")
                .WithColumn("Type").AsString(3).NotNullable()
                .WithColumn("EventTime").AsDateTime().NotNullable()
                .WithColumn("RecordTime").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("Action").AsInt16().NotNullable()
                .WithColumn("BizLocationId").AsInt32().ForeignKey("FK_EVENT_BIZLOCATION", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.BusinessLocation, "Id").Nullable()
                .WithColumn("BizStepId").AsInt32().ForeignKey("FK_EVENT_BIZSTEP", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.BusinessStep, "Id").Nullable()
                .WithColumn("DispositionId").AsInt32().ForeignKey("FK_EVENT_DISP", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.Disposition, "Id").Nullable()
                .WithColumn("ReadPointId").AsInt32().ForeignKey("FK_EVENT_READPT", DatabaseConstants.Schemas.CoreBusinessVocabulary, DatabaseConstants.Tables.ReadPoint, "Id").Nullable()
                .WithColumn("ParentId").AsString(155).ForeignKey("FK_EVT_EPC", DatabaseConstants.Schemas.Epcis, DatabaseConstants.Tables.Epc, "Id").Nullable()
                .WithColumn("Ilmd").AsXml().Nullable()
                .WithColumn("Extension").AsXml().Nullable();
        }
    }
}