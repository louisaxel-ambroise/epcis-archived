using FasTnT.Domain.Log;
using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Services.Formatting;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Modules;
using System.Diagnostics;

namespace FasTnT.DependencyInjection
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebUserAuthenticator>().To<WebUserAuthenticator>();
            Bind<IEventCapturer>().To<EventCapturer>();
            Bind<IResponseFormatter>().To<ResponseFormatter>();
            Bind<IDocumentParser>().To<DocumentParser>();


            // Logging EventLog
            string appSource = "FasTnT";
            string appLog = "Application";

            if (!EventLog.SourceExists(appSource))
            {
                EventLog.CreateEventSource(appSource, appLog);
            }

            Bind<ICaptureLogInterceptor>().To<CaptureLogInterceptor>().WithConstructorArgument("eventSource", appSource);
        }
    }
}
