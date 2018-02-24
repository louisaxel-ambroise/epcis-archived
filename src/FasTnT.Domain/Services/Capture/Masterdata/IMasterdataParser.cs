using FasTnT.Domain.Model.MasterData;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FasTnT.Domain.Services.Capture.Masterdata
{
    public interface IMasterdataParser
    {
        IEnumerable<EpcisMasterdata> Parse(XElement input);
    }
}
