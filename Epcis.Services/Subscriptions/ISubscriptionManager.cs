using System.Collections.Generic;
using Epcis.Model.Subscriptions;

namespace Epcis.Services.Subscriptions
{
    public interface ISubscriptionManager
    {
        IList<string> List();
        void Add(Subscription subscription);
        void Delete(string id);
    }
}