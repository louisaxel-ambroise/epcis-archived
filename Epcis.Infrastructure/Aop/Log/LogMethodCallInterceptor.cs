using System;
using Common.Logging;
using Ninject.Extensions.Interception;

namespace Epcis.Infrastructure.Aop.Log
{
    public class LogMethodCallInterceptor : ILogMethodCallInterceptor
    {
        private readonly ILog _logger;

        public LogMethodCallInterceptor(ILog logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");

            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Request.Method.Name;
            var methodClass = invocation.Request.Target.GetType().Name;

            _logger.Trace(string.Format("Entering method {0}.{1}", methodClass, methodName));

            try
            {
                invocation.Proceed();
                _logger.Trace(string.Format("Method {0}.{1} finished normally.", methodClass, methodName));
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Exception thrown by method {0}.{1}", methodClass, methodName), ex);

                throw;
            }
        }
    }
}