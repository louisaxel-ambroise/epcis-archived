using System;
using System.Linq;
using System.Collections.Generic;

namespace FasTnT.Domain.Model.Queries
{
    public class QueryParam
    {
        public string Name { get; set; }
        public IList<string> Values { get; set; } = new List<string>();

        public string Value => Values.Single();

        public Type ComparableType => (double.TryParse(Value, out double doubleValue)) ? typeof(double) : (DateTime.TryParse(Value, out DateTime dateValue)) ? typeof(DateTime) : null;
        public object ComparableValue => Convert.ChangeType(Value, ComparableType);
    }
}
