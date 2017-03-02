using Epcis.WebApi.DependencyInjection;
using Ninject.Modules;

namespace Epcis.WebApi
{
    public class NinjectHttpModules
    {
        public static INinjectModule[] Modules
        {
            get
            {
                return new INinjectModule[] { new EpcisModule() };
            }
        }
    }
}