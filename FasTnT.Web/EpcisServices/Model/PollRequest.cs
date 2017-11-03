using FasTnT.Domain.Model.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FasTnT.Web.EpcisServices.Model
{
    public class PollRequest
    {
        public string Name { get; set; }
        public QueryParam[] Parameters { get; set; }

        public static PollRequest Parse(XElement xmlRequest)
        {
            var poll = new PollRequest();

            foreach (var element in xmlRequest.Elements())
            {
                if (element.Name.LocalName == "name") poll.Name = element.Value;
                if (element.Name.LocalName == "params") poll.Parameters = ParseParameters(element);
            }

            return poll;
        }

        public static QueryParam[] ParseParameters(XElement element)
        {
            var parameters = new List<QueryParam>();

            foreach (var parameter in element.Elements().Where(x => x.Name.LocalName == "param"))
            {
                var param = new QueryParam();

                foreach (var elt in parameter.Elements())
                {
                    if (elt.Name.LocalName == "name") param.Name = elt.Value;
                    if (elt.Name.LocalName == "value") param.Values = ParseValues(elt);
                }

                parameters.Add(param);
            }

            return parameters.ToArray();
        }

        private static string[] ParseValues(XElement element)
        {
            if (element.HasElements)
            {
                return element.Elements().Select(x => x.Value).ToArray();
            }

            return new[] {element.Value};
        }
    }
}