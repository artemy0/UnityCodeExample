using System.Analytic.Events;

namespace System.Analytic.Core
{
    public interface IAnalyticEventSender
    {
        void SendAnalyticEvent(IAnalyticEvent analyticEvent);
    }
}