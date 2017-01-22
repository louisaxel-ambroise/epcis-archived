using Epcis.Database.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Epcis.Database
{
    public static class NHibernateSessionFactory
    {
        public static ISessionFactory CreateFactory(string connectionString)
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<BaseEventMap>())
                .BuildSessionFactory();
        }
    }
}