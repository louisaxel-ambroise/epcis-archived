using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;
using System;
using System.Collections.Generic;

namespace FasTnT.Domain.Services.Subscriptions
{
    public interface ISubscriptionManager
    {
        IEnumerable<Subscription> ListAllSubscriptions();
        void Subscribe(string subscriptionId, string queryName, IEnumerable<QueryParam> parameters, Uri destination, SubscriptionControls reportIfEmpty, SubscriptionSchedule schedule);
        void Unsubscribe(string subscriptionId);
    }
}
