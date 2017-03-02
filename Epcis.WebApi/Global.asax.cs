using System.Web.Http;
using Epcis.Data.Infrastructure;

namespace Epcis.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            StorageExtensions.Setup(); // Always call this method before starting any EPCIS application (web/desktop)

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new CustomXmlFormatter());

            GlobalConfiguration.Configure(WebApiConfig.Register);
            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);  
        }
    }
}
