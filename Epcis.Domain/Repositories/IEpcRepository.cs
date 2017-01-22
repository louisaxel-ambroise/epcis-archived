using System.Linq;
using Epcis.Domain.Model.Epcis;

namespace Epcis.Domain.Repositories
{
    public interface IEpcRepository
    {
        IQueryable<Epc> Query();
        Epc Load(string id); 
        void Store(Epc epc);
    }
}