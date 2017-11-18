using System;

namespace FasTnT.Domain.Model.Subscriptions
{
    public class SubscriptionControls
    {
        public virtual bool ReportIfEmpty { get; set; }
        public virtual DateTime InitialRecordTime { get; set; }
    }
}
