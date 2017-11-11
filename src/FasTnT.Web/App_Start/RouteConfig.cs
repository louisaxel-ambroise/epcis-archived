using FasTnT.Web.App_Start;
using FasTnT.Web.EpcisServices;
using Ninject.Extensions.Wcf;
using System.ServiceModel.Activation;
using System.Web.Mvc;
using System.Web.Routing;

namespace FasTnT.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterEpcisRoutes(RouteCollection routes)
        {
            routes.Add(new AppServiceRoute("Services/1.2/EpcisCapture", new NinjectServiceHostFactory(), typeof(CaptureService)));
            routes.Add(new AppServiceRoute("Services/1.2/EpcisQuery", new NinjectServiceHostFactory(), typeof(QueryService)));
        }
    }
}
