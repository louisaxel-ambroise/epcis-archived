namespace Epcis.Data.Storage
{
    public static class StoreCommands
    {
        public const string InsertEvent = 
            "INSERT INTO epcis.Event(EventType,EventTime,EventTimezoneOffset,CaptureTime,Action,BusinessStep,Disposition,EventId,TransformationId)" +
            "VALUES(@EventType,@EventTime,@EventTimezoneOffset,@CaptureTime,@Action,@BusinessStep,@Disposition,@EventId,@TransformationId);" +
            "SELECT SCOPE_IDENTITY();"; // Return auto-generated ID for further foreing-key inserts.

        public const string InsertExtensions =
            "INSERT INTO epcis.EventExtension(EventId, Namespace, Name, Value)" +
            "VALUES(@EventId,@Namespace,@Name,@Value);";

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
            "INSERT INTO epcis.ReadPoint(EventId,ReadPointId)" +
            "VALUES(@EventId,@ReadPointId);";

        public const string InsertBusinessLocation = 
            "INSERT INTO epcis.BusinessLocation(EventId,BusinessLocationId)" +
            "VALUES(@EventId,@BusinessLocationId);";

        public const string InsertBusinessTransaction = 
            "INSERT INTO epcis.BusinessTransaction(EventId,TransactionType,TransactionId)" +
            "VALUES(@EventId,@Type,@TransactionId);";

        public const string InsertBusinessSourceDest = 
            "INSERT INTO epcis.BusinessTransaction(EventId,TransactionType,TransactionId)" +
            "VALUES(@EventId,@Type,@SourceDestId,@Direction);";
    }
}