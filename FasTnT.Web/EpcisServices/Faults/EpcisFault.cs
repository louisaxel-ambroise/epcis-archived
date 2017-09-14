using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace FasTnT.Web.EpcisServices.Faults
{
    [DataContract]
    public class EpcisFault
    {
        public string Reason { get; set; }

        public static FaultException Create(Exception ex)
        {
            switch (ex.GetType().Name)
            {
                case "SubscribeNotPermittedException":
                    return CreateFaultException(new SubscribeNotPermittedFault { Reason = ex.Message });
                case "QueryParameterException":
                    return CreateFaultException(new QueryParameterFault { Reason = ex.Message });
                case "ImplementationException":
                    return CreateFaultException(new ImplementationFault { Reason = ex.Message });
                case "QueryTooComplexException":
                    return CreateFaultException(new QueryTooComplexFault { Reason = ex.Message });
                case "NoSuchNameException":
                    return CreateFaultException(new NoSuchNameFault { Reason = ex.Message });
                case "SecurityException":
                    return CreateFaultException(new SecurityFault { Reason = ex.Message });
                case "ValidationException":
                    return CreateFaultException(new ValidationFault { Reason = ex.Message });
                default:
                    return CreateFaultException(new EpcisFault { Reason = ex.Message });
            }
        }

        private static FaultException<T> CreateFaultException<T>(T error) where T : EpcisFault
        {
            return new FaultException<T>(error, error.Reason);
        } 
    }
}