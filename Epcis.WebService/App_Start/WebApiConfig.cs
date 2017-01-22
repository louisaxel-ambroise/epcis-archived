using System.Net.Http.Formatting;
using System.Web.Http;
using Ninject.Web.WebApi;

namespace Epcis.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Map capture XML api route
            config.Routes.MapHttpRoute("CaptureAPI", "api/v1_2/xml/capture", new { controller = "Capture" });
        }

        public static void Formatter(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new XmlMediaTypeFormatter());
        }

        public static void SetDependencyResolver(HttpConfiguration config, string connectionString, string[] xsdValidationFiles)
        {
            config.DependencyResolver = new NinjectDependencyResolver(WebServiceContainerProvider.CreateContainer(connectionString, xsdValidationFiles));
        }
    }
}
