using FasTnT.Domain.Model.Subscriptions;

namespace FasTnT.Domain.Services.Subscriptions
{
    public interface ISubscriptionScheduler
    {
        void Register(Subscription subscription);
        void Start();
        void Stop();
    }
}
