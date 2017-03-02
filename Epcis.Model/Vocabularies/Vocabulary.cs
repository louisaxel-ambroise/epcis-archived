using System.Collections.Generic;

namespace Epcis.Model.Vocabularies
{
    public class Vocabulary
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public IList<VocabularyAttribute> Attributes { get; set; }
    }
}