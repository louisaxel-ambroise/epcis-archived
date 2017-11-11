using FasTnT.DependencyInjection;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Web;
using FasTnT.Web.Helpers.Users;
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
            Bind<IAuthenticateUserInterceptor>().To<AuthenticateUserInterceptor>().InScope(_scope.Value);
            Bind<IUserSetter>().To<HttpUserContainer>().InScope(_scope.Value);
            Bind<IUserProvider>().To<HttpUserContainer>().InScope(_scope.Value);
        }
    }
}