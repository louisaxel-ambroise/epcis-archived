using FasTnT.Domain.Model.Capture;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace FasTnT.Web.EpcisServices
{
    /// <summary>
    /// Capture events/masterdata is made using REST endpoint: POST http://baseURI/EpcisCapture/
    /// </summary>
    [ServiceContract]
    public interface ICaptureService
    {
        /// <summary>
        /// Captures the EPCISDocument given in the request's body.
        /// </summary>
        /// <returns>The status of the capture.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Events")]
        CaptureResponse CaptureEvents();

        /// <summary>
        /// Captures the EPCISDocument given in the request's body.
        /// </summary>
        /// <returns>ID of captured events if the capture succeed, exception if it failed.</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Masterdata")]
        CaptureResponse CaptureMasterdata();
    }
}