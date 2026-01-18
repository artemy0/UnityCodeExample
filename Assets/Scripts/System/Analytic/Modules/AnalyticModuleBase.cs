using System.Analytic.Events;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Modules
{
    public abstract class AnalyticModuleBase : IFilteredAnalyticModule, IDisposable
    {
        private readonly CancellationTokenSource cts = new();
        private EventFilter eventFilter;

        public bool IsInitialized { get; private set; }

        public void SendAnalyticEvent(IAnalyticEvent analyticEvent)
        {
            var filtered = eventFilter?.FilterEvent(this, analyticEvent) == true;
            if (!filtered)
            {
                SendEvent(analyticEvent.Name, analyticEvent.Parameters);
            }

            LogEvent(analyticEvent.Name, analyticEvent.Parameters);
        }

        public async UniTask Initialize()
        {
            await InitializeInternal(cts.Token);
            if (cts.IsCancellationRequested)
            {
                return;
            }

            IsInitialized = true;
        }

        public abstract void SendEvent(string eventName, IReadOnlyDictionary<string, string> parameters);

        public virtual void LogEvent(string eventName, IReadOnlyDictionary<string, string> parameters)
        {
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
        }

        public void SetEventFilter(EventFilter filter)
        {
            eventFilter = filter;
        }

        protected abstract UniTask InitializeInternal(CancellationToken token);
    }
}