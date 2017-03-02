using System;
using System.Linq;
using System.Xml.Linq;
using Epcis.Infrastructure.Aop.Log;
using Epcis.Model.Exceptions;
using Epcis.Model.Queries;
using Epcis.Services.Query.EventFormatters;
using Epcis.Services.Query.Performers;

namespace Epcis.Services.Query
{
    public class XmlEventQuery : IEventQuery<XDocument>
    {
        private readonly IQueryPerformer[] _queryPerformers;
        private readonly IEventFormatter<XElement>[] _formatters;

        public XmlEventQuery(IQueryPerformer[] queryPerformers, IEventFormatter<XElement>[] formatters)
        {
            if (queryPerformers == null || !queryPerformers.Any()) throw new ArgumentNullException("queryPerformers");
            if (formatters == null || !formatters.Any()) throw new ArgumentNullException("formatters");

            _queryPerformers = queryPerformers;
            _formatters = formatters;
        }

        public string[] ListQueryNames()
        {
            return _queryPerformers.Select(x => x.Name).ToArray();
        }

        [LogMethodCall]
        public virtual XDocument Execute(string queryName, EpcisQuery parameters)
        {
            var performer = _queryPerformers.SingleOrDefault(x => x.Name == queryName);
            if (performer == null) throw new NoSuchNameException(string.Format("There is no query matching the name '{0}'", queryName));

            var events = performer.Perform(parameters);
            var formatted = from evt in events let f = _formatters.Single(x => x.CanFormat(evt)) select f.Format(evt);

            return new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"), 
                new XElement("EPCISDocument", 
                    new XAttribute("creationDate", DateTime.UtcNow), 
                    new XAttribute("schemaVersion", "1.2"), 
                    new XElement("EPCISBody", 
                        new XElement("EventList", formatted.ToArray<object>())
                    )
                )
            );
        }
    }
}