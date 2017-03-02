using System;
using System.Data;
using Ninject.Extensions.Interception;

namespace Epcis.Infrastructure.Aop.Database
{
    public class CommitTransactionInterceptor : ICommitTransactionInterceptor
    {
        private readonly IDbTransaction _transaction;

        public CommitTransactionInterceptor(IDbTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            _transaction = transaction;
        }

        public void Intercept(IInvocation invocation)
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