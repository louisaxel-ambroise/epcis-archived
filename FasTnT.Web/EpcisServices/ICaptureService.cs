using System.ServiceModel;
using System.ServiceModel.Web;

namespace FasTnT.Web.EpcisServices
{
    /// <summary>
    /// Capture events is made using REST endpoint: POST http://baseURI/EpcisCapture/
    /// </summary>
    [ServiceContract]
    public interface ICaptureService
    {
        /// <summary>
        /// Captures the EPCISDocument given in the request's body.
        /// </summary>
        /// <returns>OK if the capture succeed, exception if it failed.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/")]
        string Capture();
    }
}