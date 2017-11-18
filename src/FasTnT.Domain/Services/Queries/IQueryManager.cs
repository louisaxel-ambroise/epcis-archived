using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQueryManager
    {
        IEnumerable<string> ListQueryNames();
    }
}
