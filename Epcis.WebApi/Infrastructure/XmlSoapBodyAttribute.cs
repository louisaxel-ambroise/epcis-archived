using System.Web.Http;
using System.Web.Http.Controllers;

namespace Epcis.WebApi.Infrastructure
{
    public class XmlSoapBodyAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new XmlSoapBodyParameterBinding(parameter);
        }
    }
}