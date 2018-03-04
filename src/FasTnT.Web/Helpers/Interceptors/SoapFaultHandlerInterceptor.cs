using Ninject.Extensions.Interception;
using System;

namespace FasTnT.Web.Helpers.Attributes
{
    public class SoapFaultHandlerInterceptor : ISoapFaultHandlerInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch(Exception ex)
            {
                throw EpcisServices.Faults.EpcisFault.Create(ex);
            }
        }
    }
}
