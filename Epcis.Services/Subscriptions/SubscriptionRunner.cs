using System;
using Quartz;

namespace Epcis.Services.Subscriptions
{
    public class SubscriptionRunner : ISubscriptionRunner
    {
        private readonly IScheduler _scheduler;

        public SubscriptionRunner(IScheduler scheduler)
        {
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Start();
        }
    }
}