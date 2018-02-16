using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Repositories;
using System.Linq;
using FasTnT.Web.Features;

namespace FasTnT.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var webDashboardFeature = new WebDashboardFeature();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterEpcisRoutes(RouteTable.Routes);

            if (webDashboardFeature.IsOn())
                RouteConfig.RegisterDashboardRoutes(RouteTable.Routes);
            else
                RouteConfig.RegisterDefaultRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartScheduler();
        }

        // Start the Subscription Scheduler - You might want to change this to windows service in production
        private static void StartScheduler()
        {
            var scheduler = DependencyResolver.Current.GetService<ISubscriptionScheduler>();
            var subscriptionRepository = DependencyResolver.Current.GetService<ISubscriptionRepository>();

            foreach (var subscription in subscriptionRepository.Query().ToList())
            {
                scheduler.Register(subscription);
            }

            scheduler.Start();
        }
    }
}
