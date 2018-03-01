using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using FasTnT.Domain.Utils;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.EpcisServices.Mappings
{
    public static class Mapper
    {
        public static IEnumerable<QueryParam> MapToParameters(this IEnumerable<QueryParameter> parameters)
        {
            if (parameters == null) return new QueryParam[0];

            return parameters.Select(x => new QueryParam { Name = x.Name, Values = x.Values });
        }

        public static SubscriptionControls MapToControls(this SubscriptionControlsRequest controls)
        {
            if (controls == null) return new SubscriptionControls();

            return new SubscriptionControls {
                ReportIfEmpty = controls.ReportIfEmpty,
                InitialRecordTime = SystemContext.Clock.Now
            };
        }

        public static SubscriptionSchedule MapToSchedule(this QuerySchedule schedule)
        {
            if (schedule == null) return new SubscriptionSchedule();

            return new SubscriptionSchedule
            {
                Seconds = schedule.Second ?? string.Empty,
                Minutes = schedule.Minute ?? string.Empty,
                Hours = schedule.Hour ?? string.Empty,
                Month = schedule.Month ?? string.Empty,
                DayOfWeek = schedule.DayOfWeek ?? string.Empty,
                DayOfMonth = schedule.DayOfMonth ?? string.Empty
            };
        }
    }
}