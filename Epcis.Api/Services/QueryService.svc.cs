using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Xml.Linq;
using Epcis.Api.Faults;
using Epcis.Api.Model;
using Epcis.Services.Query;
using Epcis.Services.Subscriptions;

namespace Epcis.Api.Services
{
    /// <summary>
    /// Implementation of the IQueryService SOAP WebService.
    /// </summary>
    public class QueryService : IQueryService
    {
        private readonly ISubscriptionManager _subscriptionManager;
        private readonly IEventQuery<XDocument> _eventQuery;

        public QueryService(ISubscriptionManager subscriptionManager, IEventQuery<XDocument> eventQuery)
        {
            if (subscriptionManager == null) throw new ArgumentNullException("subscriptionManager");
            if (eventQuery == null) throw new ArgumentNullException("eventQuery");

            _subscriptionManager = subscriptionManager;
            _eventQuery = eventQuery;
        }

        public string[] GetQueryNames()
        {
            try
            {
                return _eventQuery.ListQueryNames();
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public void Subscribe(Message request)
        {
            try
            {
                var subscription = SubscriptionRequest.Parse(XElement.Parse(request.GetReaderAtBodyContents().ReadOuterXml()));

                _subscriptionManager.Add(subscription);
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public void Unsubscribe(string name)
        {
            try
            {
                _subscriptionManager.Delete(name);
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public string[] GetSubscriptionIDs()
        {
            try 
            { 
                return _subscriptionManager.List().ToArray();
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public Message Poll(Message request)
        {
            try 
            {
                var pollRequest = PollRequest.Parse(XElement.Parse(request.GetReaderAtBodyContents().ReadOuterXml()));
                var results = _eventQuery.Execute(pollRequest.Name, pollRequest.Parameters);

                return MessageResponse.CreatePollResponse(results.Root);
            }
            catch (Exception ex)
            {
                throw EpcisFault.Create(ex);
            }
        }

        public string GetVendorVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
        }

        public string GetStandardVersion()
        {
            return "1.2";
        }
    }
}
