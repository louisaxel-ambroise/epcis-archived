using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Xml.Linq;

namespace Epcis.WebApi.Infrastructure
{
    public class XmlBodyParameterBinding : HttpParameterBinding
    {
        public override bool WillReadBody { get { return true; } }

        public XmlBodyParameterBinding(HttpParameterDescriptor parameter): base(parameter)
        {
        }

        public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext context, CancellationToken cancellationToken)
        {
            var parsedBody = await DeserializeBody(context);
            context.ActionArguments[Descriptor.ParameterName] = parsedBody;
        }

        private async Task<XDocument> DeserializeBody(HttpActionContext context)
        {
            var body = await context.Request.Content.ReadAsStringAsync();

            return XDocument.Parse(body);
        }
    }
}