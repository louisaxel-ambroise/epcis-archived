using System.Collections.Generic;
using Epcis.Model.Events;
using Epcis.Model.Queries;

namespace Epcis.Services.Query.Performers
{
    public interface IQueryPerformer
    {
        string Name { get; }
        bool AllowsSubscribe { get; }

        IEnumerable<EpcisEvent> Perform(EpcisQuery query);
    }
}