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

namespace FasTnT.Web.BackgroundTasks
{
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
            _thread = Task.Run(async () =>
            {
                // Compute next executions at startup
                foreach (var subscription in _subscriptions) _scheduledExecutions.TryAdd(subscription, subscription.Schedule.GetNextOccurence(DateTime.UtcNow));

                while (!_cancellationToken.IsCancellationRequested)
                {
                    WaitTillNextExecutionOrNotification();

                    foreach (var entry in _scheduledExecutions.Where(x => x.Value <= DateTime.UtcNow))
                    {
                        _scheduledExecutions.TryUpdate(entry.Key, entry.Key.Schedule.GetNextOccurence(DateTime.UtcNow), entry.Value);
                        await Run(entry.Key).ConfigureAwait(false);
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
                    _scheduledExecutions.TryAdd(subscription, subscription.Schedule.GetNextOccurence(DateTime.UtcNow));
                }

                Monitor.Pulse(_monitor);
            }
        }

        private async Task Run(Subscription subscription)
        {
            var processor = _kernel.Get<ISubscriptionRunner>();

            await processor.Run(subscription).ConfigureAwait(false);
        }

        private void WaitTillNextExecutionOrNotification()
        {
            lock (_monitor)
            {
                var nextExecution = _scheduledExecutions.Any() ? _scheduledExecutions.Values.Min() : DateTime.UtcNow.AddSeconds(30);
                var timeUntilTrigger = nextExecution - DateTime.UtcNow;

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