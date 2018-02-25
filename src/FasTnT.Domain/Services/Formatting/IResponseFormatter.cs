using FasTnT.Domain.Model.Queries;
using FasTnT.Domain.Model.Subscriptions;

namespace FasTnT.Domain.Services.Formatting
{
    public interface IResponseFormatter
    {
        string FormatSubscriptionResponse(Subscription subscription, QueryEventResponse response);
    }
}
