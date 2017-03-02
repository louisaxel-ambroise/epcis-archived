using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Linq;
using Common.Logging;
using Epcis.Data.Queries;
using Epcis.Data.Storage;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Services.Capture;
using Epcis.Services.Capture.Parsing;
using Epcis.Services.Capture.Validation;
using Epcis.Services.Query;
using Epcis.Services.Query.Format;
using Epcis.Services.Query.Performers;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Epcis.WebApi.DependencyInjection
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class EpcisModule : NinjectModule
    {
        public override void Load()
        {
            var xsdFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "EpcisXsdFiles"));

            Bind<IValidator<XDocument>>().To<XmlDocumentValidator>().WithConstructorArgument("xsdFiles", xsdFiles);
            Bind<IEventParser<XElement>>().To<XmlElementEventParser>();
            Bind<ICapturer<XDocument>>().To<XmlDocumentCapturer>();
            Bind<IEventQuery<XDocument>>().To<XmlEventQuery>();
            Bind<ILogMethodCallInterceptor>().To<LogMethodCallInterceptor>();

            // Query performers
            Bind<IQueryPerformer<XElement>>().To<SimpleEventQueryPerformer>();
            Bind<IQueryPerformer<XElement>>().To<EpcHistoryQueryPerformer>();
            Bind<IQueryPerformer<XElement>>().To<EventByIdQueryPerformer>();

            // Event formatters
            Bind<IEventFormatter<XElement>>().To<XmlObjectEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlTransformationEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlTransactionEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlAggregationEventFormatter>();
            Bind<IEventFormatter<XElement>>().To<XmlQuantityEventFormatter>();

            Bind<IEventStore>().To<SqlEventStore>().InRequestScope();
            Bind<IEventsRetriever>().To<SqlEventsRetriever>().InRequestScope();
            Bind<IDbConnection>().ToMethod(OpenConnection).InRequestScope();

            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(ctx.Request.Target.Member.DeclaringType.FullName));
        }

        private static IDbConnection OpenConnection(IContext arg)
        {
            var connection = new SqlConnection("Server=(local);Database=epcis;Integrated Security=true;");
            connection.Open();

            return connection;
        }
    }
}