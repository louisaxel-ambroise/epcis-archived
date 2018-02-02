using FasTnT.Domain.Model.Log;

namespace FasTnT.Domain.Log
{
    public interface ILogStore
    {
        void Store(AuditLog log);
    }
}