using System.Analytic.Events;
using System.Collections.Generic;

namespace System.Analytic.Core.Cache
{
    public class AnalyticEventsCache
    {
        private readonly Queue<IAnalyticEvent> eventsCache = new();

        public bool Next(out IAnalyticEvent analyticEvent)
        {
            analyticEvent = null;

            if (eventsCache.Count <= 0)
            {
                return false;
            }

            analyticEvent = eventsCache.Dequeue();
            return true;
        }

        public void Append(IAnalyticEvent analyticEvent)
        {
            eventsCache.Enqueue(analyticEvent);
        }
    }
}