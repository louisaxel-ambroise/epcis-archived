using System.Collections.Generic;
using Epcis.Model.Vocabularies;

namespace Epcis.Services.Capture.Parsing
{
    public interface IMasterDataParser<in T>
    {
        IEnumerable<Vocabulary> Parse(T input);
    }
}