using Ninject.Modules;
using FasTnT.Domain.Repositories;
using FasTnT.Data.Repositories;
using NHibernate;
using FasTnT.Data;
using Ninject;
using FasTnT.Domain.Utils.Aspects;
using FasTnT.Data.Interceptors;

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
            requestLogger = new SQLDebugOutput();
            #endif

            Bind<IUserRepository>().To<UserRepository>().UsingScope(Scope);
            Bind<IEventRepository>().To<EventRepository>().UsingScope(Scope);

            // NHibernate Session binding
            Bind<ISessionFactory>().ToConstant(SessionProvider.SetupFactory(ConnectionString)).InSingletonScope();
            Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<ISessionFactory>().OpenSession(requestLogger)).UsingScope(Scope);
            Bind<ITransaction>().ToMethod(ctx => ctx.Kernel.Get<ISession>().BeginTransaction()).UsingScope(Scope);

            Bind<ICommitTransactionInterceptor>().To<CommitTransactionInterceptor>().UsingScope(Scope);
        }
    }
}
