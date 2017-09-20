using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;

namespace FasTnT.Domain.Log
{
    public class EmptyQueryLogInterceptor : IQueryLogInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}
