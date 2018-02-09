using FasTnT.Domain.Log;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.Dashboard.Users;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Services.Queries.Performers;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Services.Users.Dashboard;
using FasTnT.Domain.Services.Validation;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Modules;

namespace FasTnT.DependencyInjection
{
    public class DomainModule : NinjectModule
    {
        private string[] _xsdFiles;
        private IScope _scope;

        public DomainModule(IScope scope, params string[] xsdFiles)
        {
            _scope = scope;
            _xsdFiles = xsdFiles;
        }

        public override void Load()
        {
            // Dashboard Bindings
            Bind<IUserAuthenticator>().To<UserAuthenticator>();
            Bind<IUserService>().To<UserService>();
            Bind<IApiUserUpdator>().To<ApiUserUpdator>();

            // EPCIS Service Bindings
            Bind<IResponseFormatter>().To<ResponseFormatter>().InSingletonScope();
            Bind<IEventFormatter>().To<EventFormatter>().InSingletonScope();
            Bind<ISubscriptionManager>().To<SubscriptionManager>();
            Bind<IEventCapturer>().To<EventCapturer>();
            Bind<IQueryManager>().To<QueryManager>();
            Bind<IQueryPerformer>().To<QueryPerformer>();
            Bind<IRequestPersister>().To<RequestPersister>();
            Bind<IDocumentValidator>().To<XmlDocumentValidator>().WithConstructorArgument("files", _xsdFiles);
            Bind<IDocumentParser>().To<DocumentParser>();

            // Queries
            Bind<IQuery>().To<SimpleEventQuery>();

            // Subscriptions
            Bind<ISubscriptionRunner>().To<SubscriptionRunner>();

            // Logs
            Bind<ICaptureLogInterceptor>().To<CaptureLogInterceptor>();
            Bind<IQueryLogInterceptor>().To<QueryLogInterceptor>();
        }
    }
}
