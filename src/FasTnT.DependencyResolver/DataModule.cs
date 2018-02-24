using Ninject.Modules;
using FasTnT.Domain.Repositories;
using FasTnT.Data.Repositories;
using NHibernate;
using FasTnT.Data;
using Ninject;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Data.Interceptors;
using FasTnT.Domain.Services.Subscriptions;
using FasTnT.Domain.Log;
using FasTnT.Data.Log;

namespace FasTnT.DependencyInjection
{
    public class DataModule : NinjectModule
    {
        public IScope Scope;
        public string ConnectionString;

        public DataModule(IScope scope, string connectionString)
        {
            Scope = scope;
            ConnectionString = connectionString;
        }

        public override void Load()
        {
            var requestLogger = new EmptyInterceptor();

#if DEBUG
            requestLogger = new SQLDebugOutputInterceptor();
#endif

            Bind<IUserRepository>().To<UserRepository>();
            Bind<IEventRepository>().To<EventRepository>();
            Bind<ISubscriptionRepository>().To<SubscriptionRepository>();
            Bind<IAuditLogRepository>().To<AuditLogRepository>();
            Bind<IEpcisRequestRepository>().To<EpcisRequestRepository>();
            Bind<IMasterdataRepository>().To<MasterdataRepository>();

            Bind<ILogStore>().To<LogStore>();

            // NHibernate Session binding
            Bind<ISessionFactory>().ToConstant(SessionProvider.SetupFactory(ConnectionString)).InSingletonScope();
            Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession(requestLogger)).UsingScope(Scope);
            Bind<ITransaction>().ToMethod(ctx => ctx.Kernel.Get<ISession>().BeginTransaction()).UsingScope(Scope);

            // Bindings for subscription runners
            Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession(requestLogger)).WhenInjectedInto<ISubscriptionRunner>();

            Bind<ICommitTransactionInterceptor>().To<NHibernateCommitTransactionInterceptor>();
        }
    }
}
