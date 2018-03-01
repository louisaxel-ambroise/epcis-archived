using FasTnT.DependencyInjection;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Services.Users;
using FasTnT.Web.BackgroundTasks;
using FasTnT.Web.Helpers.Attributes;
using FasTnT.Web.Helpers.Users;
using Ninject.Modules;

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
            Bind<IAuthenticateUserInterceptor>().To<UserAuthenticationInterceptor>();
            Bind<ISoapFaultHandlerInterceptor>().To<SoapFaultHandlerInterceptor>();

            Bind<IUserSetter>().To<HttpUserContainer>();
            Bind<IUserProvider>().To<HttpUserContainer>();

            // For simple deployment - Bind the scheduler to WebScheduler, to be run in IIS.
            Bind<ISubscriptionScheduler>().To<WebSubscriptionScheduler>().InSingletonScope();
        }
    }
}