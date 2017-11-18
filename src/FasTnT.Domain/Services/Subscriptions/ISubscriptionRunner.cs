using FasTnT.Domain.Model.Subscriptions;
using System.Threading.Tasks;

namespace FasTnT.Domain.Services.Subscriptions
{

    public interface ISubscriptionRunner
    {
        Task Run(Subscription subscription);
    }
}
