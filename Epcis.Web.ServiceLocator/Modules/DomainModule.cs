using Epcis.Domain.Services.Capture.Events;
using Epcis.Domain.Services.Mapping;
using Ninject.Modules;

namespace Epcis.WebService.ServiceLocator.Modules
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventsCapturer>().To<EventsCapturer>();
            Bind<IEventMapper>().To<EventMapper>();
        }
    }
}