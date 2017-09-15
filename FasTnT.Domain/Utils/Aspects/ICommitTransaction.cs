using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;
using Ninject;

namespace FasTnT.Domain.Utils.Aspects
{
    public class CommitTransactionAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<ICommitTransactionInterceptor>();
        }
    }

    public interface ICommitTransactionInterceptor : IInterceptor
    {
    }
}
