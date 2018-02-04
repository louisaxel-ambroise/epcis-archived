using FasTnT.Domain.Services.Queries.Performers;

namespace FasTnT.Web.EpcisServices
{
    public class EpcisQueryService : IEpcisQueryService
    {
        private IQueryPerformer _queryPerformer;

        public EpcisQueryService(IQueryPerformer queryPerformer)
        {
            _queryPerformer = queryPerformer;
        }

        public string[] GetQueryNames(EmptyParms request)
        {
            return new[] { "Test" };
        }

        public PollResponse Query(PollRequest pollRequest)
        {
            return new PollResponse { Text = pollRequest.Params == null ? "Params are null" : $"Received {pollRequest.Params.Count} params" };
        }
    }
}
