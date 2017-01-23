using System;
using Epcis.Domain.Infrastructure;
using Ninject.Extensions.Interception;

namespace Epcis.Database.Infrastructure
{
    public class CommitTransactionInterceptor : ICommitTransactionInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommitTransactionInterceptor(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            _unitOfWork.Commit();
        }
    }
}