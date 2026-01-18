using System.Analytic.Events;
using System.Analytic.Modules;

namespace System.Analytic.Analytics.Firebase
{
    public class FirebaseEventFilter : EventFilter
    {
        public override bool FilterEvent(IAnalyticModule module, IAnalyticEvent analyticEvent)
        {
            switch (analyticEvent)
            {
                // TODO: Add event to filter
                // return true;
            }

            return false;
        }
    }
}