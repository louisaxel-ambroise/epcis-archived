using System.ServiceModel;
using FasTnT.Web.EpcisServices.Faults;
using System;

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
        [return: MessageParameter(Name = "QueryNamesResult")]
        string[] GetQueryNames([MessageParameter(Name = "QueryNamesRequest")] EmptyParms request);

        /// <summary>
        /// Returns the repository's current version
        /// </summary>
        /// <returns>The solution version</returns>
        [OperationContract(Name = "getVendorVersion")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [return: MessageParameter(Name = "VendorVersionResult")]
        string GetVendorVersion([MessageParameter(Name = "VendorVersionRequest")] EmptyParms request);

        /// <summary>
        /// Gets the EPCIS specification version implemented by the repository
        /// </summary>
        /// <returns>The implemented specification version</returns>
        [OperationContract(Name = "getStandardVersion")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationException", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [return: MessageParameter(Name = "StandardVersionResult")]
        string GetStandardVersion([MessageParameter(Name = "StandardVersionRequest")] EmptyParms request);

        /// <summary>
        /// Executes a synchronous request on the EPCIS repository
        /// </summary>
        /// <param name="queryName">The name of the query to execute</param>
        /// <param name="parameters">The parameters of the query</param>
        /// <returns>All the Events or MasterData that matches the request's parameters</returns>
        [OperationContract(Name = "poll")]
        QueryResults Poll([MessageParameter(Name = "queryName")] string queryName, [MessageParameter(Name = "params")] QueryParams parameters);

        /// <summary>
        /// Gets the list of all existing subscriptions IDs
        /// </summary>
        /// <returns>An array of string containing all the existing subscription IDs</returns>
        [OperationContract(Name = "getSubscriptionIds")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [return: MessageParameter(Name = "QueryNamesResult")]
        string[] GetSubscriptionIDs([MessageParameter(Name = "SubscriptionIDsRequest")] EmptyParms request);

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
        void Subscribe(string queryName, [MessageParameter(Name = "params")] QueryParams parameters, [MessageParameter(Name = "dest")] Uri destination, [MessageParameter(Name = "controls")] SubscriptionControls controls, [MessageParameter(Name = "subscriptionID")] string subscriptionId);

        /// <summary>
        /// Removes the specific subscription from the repository
        /// </summary>
        /// <param name="name">The name of the subscription to remove</param>
        [OperationContract(Name = "unsubscribe")]
        [FaultContract(typeof(ImplementationFault), Name = "ImplementationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(SecurityFault), Name = "SecurityExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        [FaultContract(typeof(ValidationFault), Name = "ValidationExceptionFault", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
        void Unsubscribe(string name);
    }
}
