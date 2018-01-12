using System.Collections.Generic;

namespace FasTnT.Domain.Services.Queries
{
    public interface IQueryManager
    {
        IEnumerable<string> ListQueryNames();
    }
}
