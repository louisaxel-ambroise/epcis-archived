using System;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using NHibernate;

namespace FasTnT.Data.Interceptors
{
    public class NHibernateCommitTransactionInterceptor : ICommitTransactionInterceptor
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public NHibernateCommitTransactionInterceptor(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
            _transaction = session.BeginTransaction();
        }

        public void Intercept(IInvocation invocation)
        {
            using (_transaction)
            {
                try
                {
                    invocation.Proceed();

                    _transaction.Commit();
                }
                catch
                {
                    _transaction.Rollback();

                    throw;
                }
            }
        }
    }
}
