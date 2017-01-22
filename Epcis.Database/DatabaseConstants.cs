namespace Epcis.Database
{
    public static class DatabaseConstants
    {
        public static class Schemas
        {
            public static string CoreBusinessVocabulary = "cbv";
            public static string Epcis = "epcis";
        }

        public class Tables
        {
            public static string Event = "Event";
            public static string EventToEpc = "EventToEpc";
            public static string BusinessLocation = "BizLocation";
            public static string BusinessStep = "BizStep";
            public static string ReadPoint = "ReadPoint";
            public static string Disposition = "Disposition";
            public static string Epc = "Epc";
        }
    }
}