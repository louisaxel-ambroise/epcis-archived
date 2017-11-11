using FasTnT.Data.Mappings.Users;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FasTnT.Data
{
    public static class SessionProvider
    {
        public static ISessionFactory SetupFactory(string connectionString)
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>()).BuildSessionFactory();
        }
    }
}
