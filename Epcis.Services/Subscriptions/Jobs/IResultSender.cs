namespace Epcis.Services.Subscriptions.Jobs
{
    public interface IResultSender
    {
        void SendResults<T>(string endpoint, T results);
    }
}