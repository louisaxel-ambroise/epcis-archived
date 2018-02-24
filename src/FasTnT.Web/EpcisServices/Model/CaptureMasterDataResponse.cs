using System;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices.Model
{
    [MessageContract(WrapperName = "CaptureMasterDataResponse", WrapperNamespace = "")]
    public class CaptureMasterDataResponse
    {
        [MessageBodyMember(Name = "CaptureStart", Namespace = "", Order = 0)]
        public DateTime CaptureStart { get; set; }
        [MessageBodyMember(Name = "CaptureEnd", Namespace = "", Order = 1)]
        public DateTime CaptureEnd { get; set; }
        [MessageBodyMember(Name = "MasterdataCount", Namespace = "", Order = 2)]
        public int MasterdataCount { get; set; }
        [MessageBodyMember(Name = "MasterdataIds", Namespace = "", Order = 3)]
        public string[] MasterdataIds { get; set; }
    }
}