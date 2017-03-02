using Epcis.Model.Queries;

namespace Epcis.Services.Query
{
    public interface IEventQuery<out T>
    {
        string[] ListQueryNames();
        T Execute(string queryName, EpcisQuery parameters);
    }
}