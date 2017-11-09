using FasTnT.DependencyInjection;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Web;
using Ninject.Modules;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]
namespace FasTnT.Web
{
    public class WebModule : NinjectModule
    {
        private IScope _scope;

        public WebModule(IScope scope)
        {
            _scope = scope;
        }

        public override void Load()
        {
            Bind<IBasicAuthorizationInterceptor>().To<BasicAuthorizationInterceptor>().InScope(_scope.Value);
        }
    }
}