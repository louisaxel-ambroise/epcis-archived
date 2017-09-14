using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace FasTnT.Web.App_Start
{
    public class AppServiceRoute : ServiceRoute
    {
        public AppServiceRoute(string routePrefix, ServiceHostFactoryBase serviceHostFactory, Type serviceType)
            : base(routePrefix, serviceHostFactory, serviceType)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}