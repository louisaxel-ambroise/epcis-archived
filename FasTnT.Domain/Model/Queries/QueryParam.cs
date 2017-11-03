using FasTnT.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FasTnT.Domain.Model.Queries
{
    public class QueryParam
    {
        public string Name { get; set; }
        public IEnumerable<string> Values { get; set; }

        public string Value => Values.Single();

        public Type ComparableType => (double.TryParse(Value, out double doubleValue)) ? typeof(double) : (DateTime.TryParse(Value, out DateTime dateValue)) ? typeof(DateTime) : null;
        public object ComparableValue => Convert.ChangeType(Value, ComparableType);
    }
}
