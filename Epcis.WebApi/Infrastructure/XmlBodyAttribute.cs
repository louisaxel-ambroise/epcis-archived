using System.Web.Http;
using System.Web.Http.Controllers;

namespace Epcis.WebApi.Infrastructure
{
    public class XmlBodyAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new XmlBodyParameterBinding(parameter);
        }
    }
}