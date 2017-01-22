using System.Linq;
using Epcis.Domain.Model.Epcis;

namespace Epcis.Domain.Repositories
{
    public interface IEventRepository
    {
        IQueryable<T> Query<T>() where T : BaseEvent;
        void Store(BaseEvent @event);
    }
}