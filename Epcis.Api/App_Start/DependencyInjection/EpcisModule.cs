using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web;
using System.Xml.Linq;
using Common.Logging;
using Epcis.CustomQueries;
using Epcis.Data.Queries;
using Epcis.Data.Storage;
using Epcis.Infrastructure.Aop.Database;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Services.Capture;
using Epcis.Services.Capture.Parsing;
using Epcis.Services.Capture.Validation;
using Epcis.Services.Query;
using Epcis.Services.Query.EventFormatters;
using Epcis.Services.Query.EventFormatters.Xml;
using Epcis.Services.Query.Performers;
using Epcis.Services.Subscriptions;
using Epcis.Services.Subscriptions.Jobs;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace Epcis.Api.DependencyInjection
{
    // TODO: maybe use multiple Modules?
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class EpcisModule : NinjectModule
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Epcis_SqlDatabase"].ConnectionString;

        public override void Load()
        {
            var xsdFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "EpcisXsdFiles"));

            Bind<IValidator<XDocument>>().To<XmlDocumentValidator>().WithConstructorArgument("xsdFiles", xsdFiles);
            Bind<IEventParser<XElement>>().To<XmlElementEventParser>();
            Bind<ICapturer<XDocument>>().To<XmlDocumentCapturer>();
            Bind<IEventQuery<XDocument>>().To<XmlEventQuery>();

            // AOP
            Bind<ILogMethodCallInterceptor>().To<LogMethodCallInterceptor>();
            Bind<ICommitTransactionInterceptor>().To<CommitTransactionInterceptor>();

            // Common Query performers
            Bind<IQueryPerformer>().To<SimpleEventQueryPerformer>();
            // Custom query performers
            Bind<IQueryPerformer>().To<EventByIdQueryPerformer>();
            Bind<IQueryPerformer>().To<EpcHistoryQueryPerformer>();

            // Event formatters
            Bind<IEventFormatter<XElement>>().To<XmlObjectEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlTransformationEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlTransactionEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlAggregationEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlQuantityEventFormatter>();

            // Data bindings
            Bind<IEventStore>().To<SqlEventStore>().InScope(ctx => HttpContext.Current);
            Bind<IEventsRetriever>().To<SqlEventsRetriever>().InScope(ctx => HttpContext.Current);
            Bind<IDbConnection>().ToMethod(OpenConnection).InScope(ctx => HttpContext.Current).OnDeactivation(connection => connection.Close());
            Bind<IDbTransaction>().ToMethod(ctx => ctx.Kernel.Get<IDbConnection>().BeginTransaction()).InScope(ctx => HttpContext.Current).OnDeactivation(tx => tx.Dispose());

            // Subscriptions
            Bind<ISubscriptionRunner>().To<SubscriptionRunner>();
            Bind<ISubscriptionManager>().To<SubscriptionManager>();
            // HTTP result sender for subscriptions
            Bind<IResultSender>().To<HttpResultSender>();

            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.DeclaringType.FullName));
        }

        private static IDbConnection OpenConnection(IContext arg)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}