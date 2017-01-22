using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation;
using Epcis.Database;
using Epcis.Database.Infrastructure;
using Epcis.Database.Repositories;
using Epcis.Domain.Infrastructure;
using Epcis.Domain.Repositories;
using Epcis.Domain.Services.Capture.Events;
using Epcis.Domain.Services.Mapping;
using Epcis.XmlParser;
using Epcis.XmlParser.Parsing;
using Epcis.XmlParser.Validation;
using NHibernate;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi.Filter;

namespace Epcis.WebService
{
    public static class WebServiceContainerProvider
    {
        public static IKernel CreateContainer(string connectionString, string[] xsdValidationFiles)
        {
            var kernel = new StandardKernel();

            // Database binings
            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));
            kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(new[] { new NinjectFilterProvider(kernel) }.AsEnumerable()));
            kernel.Bind<ISessionFactory>().ToConstant(NHibernateSessionFactory.CreateFactory(connectionString));
            kernel.Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<ICommitTransactionInterceptor>().To<CommitTransactionInterceptor>();
            kernel.Bind<IEventRepository>().To<EventRepository>();
            kernel.Bind<IEpcRepository>().To<EpcRepository>();
            kernel.Bind<ICoreBusinessEntityRepository>().To<CoreBusinessEntityRepository>();

            // Domain bindings
            kernel.Bind<IEventsCapturer>().To<EventsCapturer>();
            kernel.Bind<IEventMapper>().To<EventMapper>();

            // XML Processor module
            kernel.Bind<IDocumentParser>().To<DocumentParser>();
            kernel.Bind<IDocumentProcessor>().To<DocumentProcessor>();
            kernel.Bind<IDocumentValidator>().To<DocumentValidator>().WithConstructorArgument("xsdFiles", xsdValidationFiles);

            return kernel;
        }
    }
}