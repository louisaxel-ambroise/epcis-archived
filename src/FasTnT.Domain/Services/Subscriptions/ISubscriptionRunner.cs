using System;

namespace FasTnT.Domain.Services.Subscriptions
{

    public interface ISubscriptionRunner
    {
        void Run(Guid subscriptionId);
    }
}
