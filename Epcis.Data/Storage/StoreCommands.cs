namespace Epcis.Data.Storage
{
    public static class StoreCommands
    {
        public const string InsertEvent = 
            "INSERT INTO epcis.Event(EventType,EventTime,EventTimezoneOffset,CaptureTime,Action,BusinessStep,Disposition,EventId,Ilmd,TransformationId,CustomFields)" +
            "VALUES(@EventType,@EventTime,@EventTimezoneOffset,@CaptureTime,@Action,@BusinessStep,@Disposition,@EventId,@Ilmd,@TransformationId,@CustomFields);" +
            "SELECT SCOPE_IDENTITY();"; // Return auto-generated ID for further foreing-key inserts.

        public const string InsertErrorDeclaration = 
            "INSERT INTO epcis.ErrorDeclaration(EventId,DeclarationTime,Reason,CustomFields)" +
            "VALUES(@EventId,@DeclarationTime,@Reason,@CustomFields);";

        public const string InsertDeclarationIds = 
            "INSERT INTO epcis.ErrorDeclarationEventId(EventId,ReferencedId)" +
            "VALUES(@EventId,@ReferencedId);";

        public const string InsertEpc = 
            "INSERT INTO epcis.Epc(EventId,Epc,Type,IsQuantity,Quantity,UnitOfMeasure)" +
            "VALUES(@EventId,@Epc,@Type,@IsQuantity,@Quantity,@UnitOfMeasure);";

        public const string InsertReadPoint = 
            "INSERT INTO epcis.ReadPoint(EventId,ReadPointId,CustomFields)" +
            "VALUES(@EventId,@ReadPointId,@CustomFields);";

        public const string InsertBusinessLocation = 
            "INSERT INTO epcis.BusinessLocation(EventId,BusinessLocationId,CustomFields)" +
            "VALUES(@EventId,@BusinessLocationId,@CustomFields);";

        public const string InsertBusinessTransaction = 
            "INSERT INTO epcis.BusinessTransaction(EventId,TransactionType,TransactionId)" +
            "VALUES(@EventId,@Type,@TransactionId);";

        public const string InsertBusinessSourceDest = 
            "INSERT INTO epcis.BusinessTransaction(EventId,TransactionType,TransactionId,CustomFields)" +
            "VALUES(@EventId,@Type,@SourceDestId,@Direction);";
    }
}