using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

namespace Epcis.Domain.Infrastructure
{
    public class CommitTransactionAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<ICommitTransactionInterceptor>();
        }
    }
}