using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epcis.Model.Queries;

namespace Epcis.Api.Model
{
    public class PollRequest
    {
        public string Name { get; set; }
        public EpcisQuery Parameters { get; set; }

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

        public static EpcisQuery ParseParameters(XElement element)
        {
            var parameters = new List<Parameter>();

            foreach (var parameter in element.Elements().Where(x => x.Name.LocalName == "param"))
            {
                var param = new Parameter();

                foreach (var elt in parameter.Elements())
                {
                    if (elt.Name.LocalName == "name") param.Name = elt.Value;
                    if (elt.Name.LocalName == "value") param.Values = ParseValues(elt);
                }

                parameters.Add(param);
            }

            return new EpcisQuery{ Parameters = parameters.ToArray() };
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