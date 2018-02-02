using FasTnT.Domain.Model.Log;
using FasTnT.Domain.Services.Users;
using FasTnT.Domain.Utils;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using System;
using System.Diagnostics;

namespace FasTnT.Domain.Log
{
    public class QueryLogInterceptor : IQueryLogInterceptor
    {
        private readonly ILogStore _logStore;
        private readonly IUserProvider _userProvider;

        public QueryLogInterceptor(ILogStore logStore, IUserProvider userProvider)
        {
            _logStore = logStore;
            _userProvider = userProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                watch.Stop();

                var user = _userProvider.GetCurrentUser();
                var log = new AuditLog { Action = LogAction.Query, Timestamp = SystemContext.Clock.Now, User = user, ExecutionTimeMs = watch.ElapsedMilliseconds };

                if (watch.ElapsedMilliseconds > TimeSpan.FromSeconds(10).TotalMilliseconds)
                {
                    log.Description = "QuerySucceedTimeLimit";
                    log.Type = "Warning";
                }
                else
                {
                    log.Description = "QuerySucceed";
                    log.Type = "Information";
                }

                _logStore.Store(log);
            }
            catch
            {
                watch.Stop();
                var user = _userProvider.GetCurrentUser();
                _logStore.Store(new AuditLog
                {
                    Action = LogAction.Capture,
                    Timestamp = SystemContext.Clock.Now,
                    User = user,
                    Description = "QueryFailed",
                    Type = "Error",
                    ExecutionTimeMs = watch.ElapsedMilliseconds
                });

                throw;
            }
        }
    }
}
