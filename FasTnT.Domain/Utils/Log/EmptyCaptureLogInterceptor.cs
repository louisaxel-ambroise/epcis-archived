using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;

namespace FasTnT.Domain.Log
{
    public class EmptyCaptureLogInterceptor : ICaptureLogInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
