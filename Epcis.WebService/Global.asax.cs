using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Epcis.WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EpcisDatabase"].ConnectionString;
            var xsdValidators = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"/App_Data/");

            GlobalConfiguration.Configure(config =>
            {
                WebApiConfig.Register(config);
                WebApiConfig.Formatter(config);
                WebApiConfig.SetDependencyResolver(config, connectionString, xsdValidators);
            });
        }
    }
}
