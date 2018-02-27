using FasTnT.Domain.Model.MasterData;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Web.Models.Masterdata
{
    public static class Mappings
    {
        public static IEnumerable<MasterdataViewModel> MapToViewModel(this IQueryable<EpcisMasterdata> source)
        {
            return source.Select(x => new MasterdataViewModel
            {
                Type = x.Type,
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                LastUpdatedOn = x.LastUpdatedOn,
                AttributesCount = x.Attributes.Count
            });
        }
    }
}