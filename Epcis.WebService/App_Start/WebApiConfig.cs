using System.Net.Http.Formatting;
using System.Web.Http;
using Epcis.WebService.ServiceLocator;
using Ninject.Web.WebApi;

namespace Epcis.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        public static void Formatter(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new XmlMediaTypeFormatter());
        }

        public static void SetDependencyResolver(HttpConfiguration config, string connectionString, string[] xsdValidationFiles)
        {
            config.DependencyResolver = new NinjectDependencyResolver(ServiceProvider.CreateKernel(connectionString, xsdValidationFiles));
        }
    }
}
