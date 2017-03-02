using System;

namespace Epcis.Model.Subscriptions
{
    public class SubscriptionControls
    {
        public string Schedule { get; set; }
        public string Trigger { get; set; }
        public bool ReportIfEmpty { get; set; }
        public DateTime InitialRecordTime { get; set; }
    }
}