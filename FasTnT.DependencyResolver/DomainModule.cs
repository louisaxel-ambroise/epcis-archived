using FasTnT.Domain.Services.Dashboard;
using FasTnT.Domain.Services.EventCapture;
using FasTnT.Domain.Services.Formatting;
using Ninject.Modules;

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
        }
    }
}
