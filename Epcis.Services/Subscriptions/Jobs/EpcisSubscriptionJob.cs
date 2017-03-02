using System;
using System.Xml.Linq;
using Epcis.Model.Queries;
using Epcis.Services.Query;
using Newtonsoft.Json;
using Quartz;

namespace Epcis.Services.Subscriptions.Jobs
{
    [DisallowConcurrentExecution]
    public class EpcisSubscriptionJob : IJob
    {
        private readonly IEventQuery<XDocument> _eventQuery;
        private readonly IResultSender _resultSender;

        public EpcisSubscriptionJob(IEventQuery<XDocument> eventQuery, IResultSender resultSender)
        {
            if (eventQuery == null) throw new ArgumentNullException("eventQuery");
            if (resultSender == null) throw new ArgumentNullException("resultSender");

            _eventQuery = eventQuery;
            _resultSender = resultSender;
        }

        public void Execute(IJobExecutionContext context)
        {
            var queryName = context.MergedJobDataMap.GetString("QueryName");
            var parameters = JsonConvert.DeserializeObject<EpcisQuery>(context.MergedJobDataMap.GetString("Parameters"));
            var destination = context.MergedJobDataMap.GetString("Endpoint");

            var results = _eventQuery.Execute(queryName, parameters);

            _resultSender.SendResults(destination, results);
        }
    }
}