using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using System;
using System.Diagnostics;

namespace FasTnT.Domain.Log
{
    public class QueryLogInterceptor : IQueryLogInterceptor
    {
        private readonly EventLog _eventLog;

        public QueryLogInterceptor(EventLog eventLog)
        {
            _eventLog = eventLog ?? throw new ArgumentException(nameof(eventLog));
        }

        public void Intercept(IInvocation invocation)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                watch.Stop();

                if(watch.ElapsedMilliseconds > TimeSpan.FromSeconds(10).TotalMilliseconds)
                {
                    _eventLog.WriteEntry("QuerySucceedTooLong", EventLogEntryType.Warning);
                }
                else
                {
                    _eventLog.WriteEntry("QuerySucceed", EventLogEntryType.Information);
                }
            }
            catch
            {
                _eventLog.WriteEntry("QueryFailed", EventLogEntryType.Error);
                throw;
            }
        }
    }
}
