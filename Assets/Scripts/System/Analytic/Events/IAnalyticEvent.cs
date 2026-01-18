using System.Collections.Generic;

namespace System.Analytic.Events
{
    public interface IAnalyticEvent : IDisposable
    {
        string Name { get; }
        IReadOnlyDictionary<string, string> Parameters { get; }

        void Release();
    }
}