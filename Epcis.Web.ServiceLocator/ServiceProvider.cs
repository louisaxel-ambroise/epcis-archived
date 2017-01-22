using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation;
using Epcis.WebService.ServiceLocator.Modules;
using Ninject;
using Ninject.Web.WebApi.Filter;

namespace Epcis.WebService.ServiceLocator
{
    public class ServiceProvider
    {
        public static IKernel CreateKernel(string connectionString, string[] xsdValidationFiles)
        {
            var kernel = new StandardKernel(
                new DatabaseModule(connectionString),
                new DomainModule(),
                new XmlProcessorModule(xsdValidationFiles)
            );

            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));
            kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(new[] { new NinjectFilterProvider(kernel) }.AsEnumerable()));
            return kernel;
        }
    }
}
