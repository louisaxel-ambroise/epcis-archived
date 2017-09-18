using System;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using NHibernate;

namespace FasTnT.Data.Interceptors
{
    public class CommitTransactionInterceptor : ICommitTransactionInterceptor
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public CommitTransactionInterceptor(ISession session)
        {
            _session = session ?? throw new ArgumentException(nameof(session));
            _transaction = session.BeginTransaction();
        }

        public void Intercept(IInvocation invocation)
        {
            using (_transaction)
            {
                invocation.Proceed();

                _transaction.Commit();
            }
        }
    }
}
