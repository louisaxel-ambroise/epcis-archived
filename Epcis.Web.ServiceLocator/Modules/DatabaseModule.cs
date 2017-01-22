using Epcis.Database;
using Epcis.Database.Infrastructure;
using Epcis.Database.Repositories;
using Epcis.Domain.Infrastructure;
using Epcis.Domain.Repositories;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Epcis.WebService.ServiceLocator.Modules
{
    public class DatabaseModule : NinjectModule
    {
        private readonly string _connectionString;

        public DatabaseModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<ISessionFactory>().ToConstant(NHibernateSessionFactory.CreateFactory(_connectionString));
            Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<ICommitTransactionInterceptor>().To<CommitTransactionInterceptor>();

            Bind<IEventRepository>().To<EventRepository>();
            Bind<IEpcRepository>().To<EpcRepository>();
            Bind<ICoreBusinessEntityRepository>().To<CoreBusinessEntityRepository>();
        }
    }
}