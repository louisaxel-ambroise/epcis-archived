using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Epcis.Api.DependencyInjection;
using Epcis.Api.Services;
using Epcis.Infrastructure.Storage;
using Epcis.Services.Subscriptions;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;

namespace Epcis.Api
{
    public class Global : NinjectHttpApplication
    {
        private ISubscriptionRunner _subscriptionRunner;
        private IKernel _kernel;

        protected override IKernel CreateKernel()
        {
            _kernel = new StandardKernel(new EpcisModule(), new QuartzModule());

            _kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            _kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            return _kernel;
        }
        
        protected override void OnApplicationStarted()
        {
            RegisterRoutes();
            StorageExtensions.Setup(); // Always call this method before starting any EPCIS application

            // Start the Subscription Runner.
            _subscriptionRunner = _kernel.Get<ISubscriptionRunner>();
            _subscriptionRunner.Start();
        }

        protected override void OnApplicationStopped()
        {
            if (_subscriptionRunner != null)
            {
                _subscriptionRunner.Stop();
            }
            if (_kernel != null && !_kernel.IsDisposed)
            {
                _kernel.Dispose();
            }
        }

        private static void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("EpcisCapture", new NinjectServiceHostFactory(), typeof(CaptureService)));
            RouteTable.Routes.Add(new ServiceRoute("EpcisQuery", new NinjectServiceHostFactory(), typeof(QueryService)));
        }
    }
}