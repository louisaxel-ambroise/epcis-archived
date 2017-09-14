using FasTnT.Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FasTnT.Data
{
    public static class SessionProvider
    {
        public static ISessionFactory SetupFactory(string connectionString)
        {
            return Fluently.Configure().Database(PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(c => c
                        .Host("localhost")
                        .Port(5432)
                        .Database("fastnt")
                        .Username("postgres")
                        .Password("2*15grt22")))
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UserMap>()).BuildSessionFactory();
        }
    }
}
