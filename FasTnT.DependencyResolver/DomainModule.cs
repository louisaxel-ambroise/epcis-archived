using FasTnT.Domain.Log;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Domain.Services.Queries;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Services.Validation;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Modules;
using System.Diagnostics;

namespace FasTnT.DependencyInjection
{
    public class DomainModule : NinjectModule
    {
        private string[] _xsdFiles;

        public DomainModule(params string[] xsdFiles)
        {
            _xsdFiles = xsdFiles;
        }

        public override void Load()
        {
            Bind<IWebUserAuthenticator>().To<WebUserAuthenticator>();
            Bind<IResponseFormatter>().To<ResponseFormatter>().InSingletonScope();
            Bind<IEventFormatter>().To<EventFormatter>().InSingletonScope();
            Bind<ISubscriptionManager>().To<SubscriptionManager>();
            Bind<IEventCapturer>().To<EventCapturer>();
            Bind<IQueryPerformer>().To<QueryPerformer>();
            Bind<IEventPersister>().To<EventPersister>();
            Bind<IDocumentValidator>().To<XmlDocumentValidator>().WithConstructorArgument("files", _xsdFiles);
            Bind<IDocumentParser>().To<DocumentParser>();

            // Queries
            Bind<IQuery>().To<SimpleEventQuery>();

            // Logging EventLog
#if EVENT_LOG
            string appSource = "FasTnT";
            string appLog = "Application";

            if (!EventLog.SourceExists(appSource))
            {
                EventLog.CreateEventSource(appSource, appLog);
            }

            Bind<ICaptureLogInterceptor>().To<CaptureLogInterceptor>().WithConstructorArgument("eventSource", appSource);
            Bind<IQueryLogInterceptor>().To<QueryLogInterceptor>().WithConstructorArgument("eventSource", appSource);
#else
            Bind<ICaptureLogInterceptor>().To<EmptyCaptureLogInterceptor>();
            Bind<IQueryLogInterceptor>().To<EmptyQueryLogInterceptor>();
#endif
        }
    }
}
