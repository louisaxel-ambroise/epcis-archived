using System;
using System.Data;
using NHibernate;

namespace Epcis.Database.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITransaction _transaction;
        public ISession Session { get; }

        public UnitOfWork(ISession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            Session = session;
            _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}