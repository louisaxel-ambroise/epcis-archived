using System;
using NHibernate;

namespace Epcis.Database.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }
        void Commit();
        void Rollback();
    }
}