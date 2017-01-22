using System.Linq;
using Epcis.Domain.Model.CoreBusinessVocabulary;

namespace Epcis.Domain.Repositories
{
    public interface ICoreBusinessEntityRepository
    {
        IQueryable<T> Query<T>() where T : CoreBusinessEntity;
        T LoadWithName<T>(string name) where T : CoreBusinessEntity;
        T Load<T>(int id) where T : CoreBusinessEntity;
        void Store<T>(T cbv) where T : CoreBusinessEntity;
    }
}