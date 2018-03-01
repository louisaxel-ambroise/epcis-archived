using System.Collections.Generic;

namespace FasTnT.Web.Models.Masterdata
{
    public class MasterdataSummary
    {
        public IEnumerable<MasterdataViewModel> MasterData { get; set; }
        public int TotalMasterDataCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}