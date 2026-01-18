using System.Analytic.Events;

namespace System.Analytic.Modules
{
    public abstract class EventFilter
    {
        public abstract bool FilterEvent(IAnalyticModule module, IAnalyticEvent analyticEvent);
    }
}