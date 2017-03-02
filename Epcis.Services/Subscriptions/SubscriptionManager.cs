using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Epcis.Model.Exceptions;
using Epcis.Model.Subscriptions;
using Epcis.Services.Query.Performers;
using Epcis.Services.Subscriptions.Jobs;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl.Matchers;

namespace Epcis.Services.Subscriptions
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly IQueryPerformer[] _queryPerformers;
        private readonly IScheduler _scheduler;

        public SubscriptionManager(IQueryPerformer[] queryPerformers, IScheduler scheduler)
        {
            if (queryPerformers == null || !queryPerformers.Any()) throw new ArgumentNullException("queryPerformers");
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            _queryPerformers = queryPerformers;
            _scheduler = scheduler;
        }

        public IList<string> List()
        {
            return _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals("Epcis.Subscriptions")).Select(x => x.Name).ToList();
        }

        public void Delete(string id)
        {
            _scheduler.DeleteJob(new JobKey(id, "Epcis.Subscriptions"));
        }

        public void Add(Subscription subscription)
        {
            EnsureQueryAllowsSubscription(subscription);

            var jobData = CreateJobData(subscription);

            var jobDetails = JobBuilder.Create<EpcisSubscriptionJob>()
                        .WithIdentity(subscription.Id, "Epcis.Subscriptions")
                        .SetJobData(new JobDataMap(jobData))
                        .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(subscription.Id, "Epcis.Subscriptions")
                .WithSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(60))
                .StartAt(DateTimeOffset.UtcNow)
                .Build();

            _scheduler.ScheduleJob(jobDetails, trigger);
        }

        private void EnsureQueryAllowsSubscription(Subscription subscription)
        {
            var performer = _queryPerformers.SingleOrDefault(x => x.Name == subscription.QueryName);

            if (performer == null) throw new NoSuchNameException(subscription.QueryName);
            if (!performer.AllowsSubscribe) throw new SubscribeNotPermittedException(subscription.QueryName);
        }

        private static IDictionary CreateJobData(Subscription subscription)
        {
            return new Dictionary<string, object>
            {
                { "QueryName", subscription.QueryName },
                { "Endpoint", subscription.Destination },
                { "ReportIfEmpty", subscription.Controls.ReportIfEmpty.ToString() },
                { "Parameters", JsonConvert.SerializeObject(subscription.Parameters) }
            };
        }
    }
}