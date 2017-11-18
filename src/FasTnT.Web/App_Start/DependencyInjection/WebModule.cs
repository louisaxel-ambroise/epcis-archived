using FasTnT.DependencyInjection;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Web;
using FasTnT.Web.BackgroundTasks;
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
            Bind<IAuthenticateUserInterceptor>().To<AuthenticateUserInterceptor>();
            Bind<IUserSetter>().To<HttpUserContainer>();
            Bind<IUserProvider>().To<HttpUserContainer>();

            Bind<ISubscriptionScheduler>().To<WebSubscriptionScheduler>().InSingletonScope();
        }
    }
}