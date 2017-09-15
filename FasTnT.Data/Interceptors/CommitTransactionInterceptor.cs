using System;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using NHibernate;

namespace FasTnT.Data.Interceptors
{
    public class CommitTransactionInterceptor : ICommitTransactionInterceptor
    {
        private readonly ITransaction _transaction;

        public CommitTransactionInterceptor(ITransaction transaction)
        {
            if(transaction == null || transaction.WasCommitted ||transaction.WasRolledBack || !transaction.IsActive)
            {
                throw new ArgumentException(nameof(transaction));
            }

            _transaction = transaction ?? throw new ArgumentException(nameof(transaction));
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
