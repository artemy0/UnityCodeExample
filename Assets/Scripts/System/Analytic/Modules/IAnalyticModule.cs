using System.Analytic.Core;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Modules
{
    public interface IAnalyticModule : IAnalyticEventSender
    {
        bool IsInitialized { get; }
        UniTask Initialize();
        void SendEvent(string eventName, IReadOnlyDictionary<string, string> parameters);
    }
}