using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;

namespace FasTnT.Data.Tests.MappingTests
{
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration configuration;
        private static ISessionFactory SessionFactory;
        protected ISession session { get; set; }

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            SessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(assemblyContainingMapping))
                .ExposeConfiguration(x => configuration = x)
                .BuildSessionFactory();

            session = SessionFactory.OpenSession();

            SchemaExport export = new SchemaExport(configuration);
            export.Execute(true, true, false, session.Connection, null);
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
