using System.ServiceModel;
using System.ServiceModel.Channels;
using FasTnT.Web.EpcisServices.Faults;

namespace FasTnT.Web.EpcisServices
{
    /// <summary>
    /// SOAP webservice for implementation of the EPCIS query as described in the EPCGlobal EPCIS 1.2 specification
    /// </summary>
    [ServiceContract(Name = "EpcisQuery", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
    public interface IQueryService
    {
        /// <summary>
        /// List all the query names available in this EPCIS deployment
        /// </summary>
        /// <returns>An array of string containing all the query names</returns>
        [OperationContract(Name = "getQueryNames")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        string[] GetQueryNames();

        /// <summary>
        /// Creates a subscription that refers to a query, and the results will be sent periodically to the receiver
        /// </summary>
        /// <param name="request">The subscription request</param>
        [OperationContract(Name = "subscribe")]
        [FaultContract(typeof(NoSuchNameFault), Name = "NoSuchNameExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(InvalidUriFault), Name = "InvalidUriExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(DuplicateSubscriptionFault), Name = "DuplicateSubscriptionExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(QueryParameterFault), Name = "QueryParameterExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(QueryTooComplexFault), Name = "QueryTooComplexExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SubscribeNotPermittedFault), Name = "SubscribeNotPermittedExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SubscriptionControlsFault), Name = "SubscriptionControlsExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        void Subscribe(Message request);

        /// <summary>
        /// Removes the specific subscription from the repository
        /// </summary>
        /// <param name="name">The name of the subscription to remove</param>
        [OperationContract(Name = "unsubscribe")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        void Unsubscribe(string name);

        /// <summary>
        /// Gets the list of all existing subscriptions IDs
        /// </summary>
        /// <returns>An array of string containing all the existing subscription IDs</returns>
        [OperationContract(Name = "getSubscriptionIds")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        string[] GetSubscriptionIDs();

        /// <summary>
        /// Executes a synchronous request on the EPCIS repository
        /// </summary>
        /// <param name="request">The request to execute on the repository</param>
        /// <returns>All the Events or MasterData that matches the request's parameters</returns>
        [OperationContract(Name = "poll")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        Message Poll(Message request);

        /// <summary>
        /// Gets the EPCIS specification version implemented by the repository
        /// </summary>
        /// <returns>The implemented specification version</returns>
        [OperationContract(Name = "getStandardVersion")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        string GetStandardVersion();

        /// <summary>
        /// Returns the repository's current version
        /// </summary>
        /// <returns>The solution version</returns>
        [OperationContract(Name = "getVendorVersion")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        string GetVendorVersion();
    }
}
