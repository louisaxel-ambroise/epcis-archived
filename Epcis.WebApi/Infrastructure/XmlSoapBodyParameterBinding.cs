using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Xml.Linq;
using Epcis.Model.Query;

namespace Epcis.WebApi.Infrastructure
{
    public class XmlSoapBodyParameterBinding : HttpParameterBinding
    {
        public XmlSoapBodyParameterBinding(HttpParameterDescriptor parameter) : base(parameter)
        {
        }

        public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext context, CancellationToken cancellationToken)
        {
            var parsedBody = await DeserializeBody(context);
            context.ActionArguments[Descriptor.ParameterName] = parsedBody;
        }

        private static async Task<EpcisQuery<XElement>> DeserializeBody(HttpActionContext context)
        {
            var body = await context.Request.Content.ReadAsStringAsync();
            var document = XDocument.Parse(body);

            if (document.Root == null) return null;
            if (document.Root.Name.LocalName != "Envelope") return null;
            var envelop = document.Root;
            var envelopBody = envelop.Elements().First();

            if (envelopBody == null) return null;

            var methodCall = envelopBody.Elements().Single();

            return new EpcisQuery<XElement>
            {
                Name = methodCall.Name.LocalName,
                Parameters = methodCall
            };
        }
    }
}