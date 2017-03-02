using Epcis.Infrastructure.QuartzNinject;
using Epcis.Services.Subscriptions.Jobs;
using Ninject;
using Ninject.Modules;
using Quartz;
using Quartz.Impl;

namespace Epcis.Api.DependencyInjection
{
    public class QuartzModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISchedulerFactory>().ToConstant(new StdSchedulerFactory());
            Bind<IJob>().To<EpcisSubscriptionJob>();
            Bind<IScheduler>().ToMethod(ctx =>
            {
                var factory = ctx.Kernel.Get<ISchedulerFactory>().GetScheduler();
                factory.JobFactory = new NinjectJobFactory(ctx.Kernel);

                return factory;
            }).InSingletonScope();
        }
    }
}