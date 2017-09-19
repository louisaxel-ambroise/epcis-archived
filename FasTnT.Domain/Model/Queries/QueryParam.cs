using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Model.Queries
{
    public class QueryParam
    {
        public string Name { get; set; }
        public IEnumerable<string> Values { get; set; }

        public string Value => Values.Single();
    }
}
