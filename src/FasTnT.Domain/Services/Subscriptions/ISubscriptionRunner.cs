using System.Threading.Tasks;

namespace FasTnT.Domain.Services.Subscriptions
{

    public interface ISubscriptionRunner
    {
        void Run(string subscriptionId);
    }
}
