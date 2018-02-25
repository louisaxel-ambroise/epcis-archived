using System;
using FasTnT.Domain.Services.Subscriptions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FasTnT.Domain.Model.Subscriptions;
using System.Linq;
using Ninject;
using System.Web.Hosting;
using FasTnT.Domain.Utils;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace FasTnT.Web.BackgroundTasks
{
    // WebSubscriptionScheduler: runs the subscription thread in the Web context.
    // WARNING: this code is not suitable for any production environment.
    // For a regular deployment, subscriptions will need to run in another process than the web interface.
    public class WebSubscriptionScheduler : ISubscriptionScheduler, IRegisteredObject
    {
        public bool IsRunning { get; set; }

        private IKernel _kernel;
        private CancellationTokenSource _cancellationToken;
        private Task _thread;
        private volatile object _monitor;
        private ConcurrentDictionary<Subscription, DateTime> _scheduledExecutions;
        private IList<Subscription> _subscriptions;

        public WebSubscriptionScheduler(IKernel kernel)
        {
            HostingEnvironment.RegisterObject(this);

            _kernel = kernel;
            _monitor = new object();
            _cancellationToken = new CancellationTokenSource();
            _scheduledExecutions = new ConcurrentDictionary<Subscription, DateTime>();
            _subscriptions = new List<Subscription>();
        }

        public void Start()
        {
            IsRunning = true;
            _thread = Task.Run(() =>
            {
                // Compute next executions at startup
                foreach (var subscription in _subscriptions) _scheduledExecutions.TryAdd(subscription, subscription.Schedule.GetNextOccurence(SystemContext.Clock.Now));

                while (!_cancellationToken.IsCancellationRequested)
                {
                    WaitTillNextExecutionOrNotification();

                    foreach (var entry in _scheduledExecutions.Where(x => x.Value <= SystemContext.Clock.Now))
                    {
                        _scheduledExecutions.TryUpdate(entry.Key, entry.Key.Schedule.GetNextOccurence(SystemContext.Clock.Now), entry.Value);
                        Run(entry.Key);
                    }
                }
            }, _cancellationToken.Token);
            _thread.ConfigureAwait(false);
        }

        public void Register(Subscription subscription)
        {
            lock (_monitor)
            {
                _subscriptions.Add(subscription);
                if (IsRunning)
                {
                    _scheduledExecutions.TryAdd(subscription, subscription.Schedule.GetNextOccurence(SystemContext.Clock.Now));
                }

                Monitor.Pulse(_monitor);
            }
        }

        private void Run(Subscription subscription)
        {
            try
            {
                // This hack allows to bind the same ISession with Ninject in the rependencies of ISubscriptionRunner.
                HttpContext.Current = new HttpContext(new HttpRequest("", "http://tempuri.org/fastnt", ""), new HttpResponse(new StringWriter()));

                var processor = _kernel.Get<ISubscriptionRunner>();
                processor.Run(subscription.Id);

                HttpContext.Current = null;
            }
            catch(Exception ex)
            {
                // TODO: do something more intelligent
                Trace.WriteLine($"Subscription {subscription.Id} callback failed: {ex.Message}");
            }
        }

        private void WaitTillNextExecutionOrNotification()
        {
            lock (_monitor)
            {
                var nextExecution = _scheduledExecutions.Any() ? _scheduledExecutions.Values.Min() : SystemContext.Clock.Now.AddSeconds(30);
                var timeUntilTrigger = nextExecution - SystemContext.Clock.Now;

                if (timeUntilTrigger > TimeSpan.Zero)
                {
                    Monitor.Wait(_monitor, timeUntilTrigger);
                }
            }
        }

        public void Stop()
        {
            Stop(false);
        }

        public void Stop(bool immediate)
        {
            _cancellationToken.Cancel();

            if (_thread != null)
            {
                _thread.Wait(TimeSpan.FromSeconds(10));
            }

            IsRunning = false;
            HostingEnvironment.UnregisterObject(this);
        }
    }
}