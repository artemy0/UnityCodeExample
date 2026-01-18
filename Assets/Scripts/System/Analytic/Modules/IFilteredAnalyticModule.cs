namespace System.Analytic.Modules
{
    public interface IFilteredAnalyticModule : IAnalyticModule
    {
        void SetEventFilter(EventFilter filter);
    }
}