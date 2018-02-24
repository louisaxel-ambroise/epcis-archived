using FasTnT.Domain.Exceptions;
using FasTnT.Domain.Model.MasterData;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.MasterdataCapture
{
    public class MasterdataParser : IMasterdataParser
    {
        public IEnumerable<EpcisMasterdata> Parse(XElement input)
        {
            const string EpcisMasterdataNamespace = "urn:epcglobal:epcis-masterdata:xsd:1";

            if (input.Name.Equals(XName.Get("EPCISMasterDataDocument", EpcisMasterdataNamespace)))
            {
                return ParseMasterData(input.Element("EPCISBody").Element("VocabularyList"));
            }

            throw new EpcisException($"Unexpected XML element : '{input.Name.LocalName}'");
        }

        private IEnumerable<EpcisMasterdata> ParseMasterData(XElement input)
        {
            var masterdata = new List<EpcisMasterdata>();

            foreach(var element in input.Elements("Vocabulary"))
            {
                var type = element.Attribute("type").Value;

                masterdata.AddRange(ParseMasterDataList(element, type));
            }

            return masterdata;
        }

        private IEnumerable<EpcisMasterdata> ParseMasterDataList(XElement element, string type)
        {
            foreach(var vocabulary in element.Element("VocabularyElementList").Elements("VocabularyElement"))
            {
                yield return ParseMasterData(vocabulary, type);
            }
        }

        private EpcisMasterdata ParseMasterData(XElement vocabulary, string type)
        {
            var masterdata = new EpcisMasterdata
            {
                Id = vocabulary.Attribute("id").Value,
                Type = type
            };

            foreach(var attribute in vocabulary.Elements("attribute"))
            {
                masterdata.Attributes.Add(new MasterdataAttribute
                {
                    Id = attribute.Attribute("id").Value,
                    MasterData = masterdata,
                    Value = attribute.Value
                });
            }

            return masterdata;
        }
    }
}
