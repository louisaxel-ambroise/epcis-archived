using System.Collections.Generic;

namespace FasTnT.Domain.Model.Queries
{
    public class QueryParam
    {
        public string Name { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}
