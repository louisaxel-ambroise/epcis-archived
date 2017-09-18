using System;
using FasTnT.Domain.Utils.Aspects;
using Ninject.Extensions.Interception;
using System.Diagnostics;

namespace FasTnT.Domain.Log
{
    public class CaptureLogInterceptor : ICaptureLogInterceptor
    {
        private readonly EventLog _eventLog;

        public CaptureLogInterceptor(EventLog eventLog, string eventSource)
        {
            _eventLog = eventLog ?? throw new ArgumentException(nameof(eventLog));
            _eventLog.Source = eventSource;
        }

        public void Intercept(IInvocation invocation)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
                watch.Stop();

                if (watch.ElapsedMilliseconds > TimeSpan.FromSeconds(10).TotalMilliseconds)
                {
                    _eventLog.WriteEntry("CaptureSucceedTimeLimit", EventLogEntryType.Warning);
                }
                else
                {
                    _eventLog.WriteEntry("CaptureSucceed", EventLogEntryType.Information);
                }
            }
            catch 
            {
                _eventLog.WriteEntry("CaptureFailed", EventLogEntryType.Error);

                throw;
            }
        }
    }
}
