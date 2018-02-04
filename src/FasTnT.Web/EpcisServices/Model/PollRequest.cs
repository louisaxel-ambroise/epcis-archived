using FasTnT.Domain.Model.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Web.EpcisServices.Model
{
    public class PollRequest
    {
        public string Name { get; set; }
        public Domain.Model.Queries.QueryParam[] Parameters { get; set; }

        public static PollRequest Parse(XElement xmlRequest)
        {
            var poll = new PollRequest();

            foreach (var element in xmlRequest.Elements())
            {
                if (element.Name.LocalName == "name") poll.Name = element.Value;
                else if (element.Name.LocalName == "params") poll.Parameters = ParseParameters(element);
                else throw new System.Exception($"Element '{element.Name.LocalName}' is not expected here.");
            }

            return poll;
        }

        public static Domain.Model.Queries.QueryParam[] ParseParameters(XElement element)
        {
            var parameters = new List<Domain.Model.Queries.QueryParam >();

            foreach (var parameter in element.Elements().Where(x => x.Name.LocalName == "param"))
            {
                var param = new Domain.Model.Queries.QueryParam();

                foreach (var elt in parameter.Elements())
                {
                    if (elt.Name.LocalName == "name") param.Name = elt.Value;
                    else if (elt.Name.LocalName == "value") param.Values.Add(elt.Value);
                    else throw new System.Exception($"Element '{elt.Name.LocalName}' is not expected here.");
                }

                parameters.Add(param);
            }

            return parameters.ToArray();
        }
    }
}