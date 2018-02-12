using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;
using Ninject.Extensions.Interception.Attributes;
using Ninject;

namespace FasTnT.Web.Helpers.Attributes
{
    public class AuthenticateUserAttribute : InterceptAttribute
    {
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Kernel.Get<IAuthenticateUserInterceptor>();
        }
    }
}
