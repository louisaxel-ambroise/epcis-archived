using Ninject;
using Ninject.Syntax;
using Quartz;
using Quartz.Spi;

namespace Epcis.Infrastructure.QuartzNinject
{
    /// <summary>
    /// Allows to use Ninject for instanciating Quartz.Net jobs
    /// </summary>
    public class NinjectJobFactory : IJobFactory
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectJobFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _resolutionRoot.Get<IJob>();
        }

        public void ReturnJob(IJob job)
        {
            _resolutionRoot.Release(job);
        }
    }
}