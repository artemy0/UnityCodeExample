using Cysharp.Threading.Tasks;

namespace System.Analytic.Core
{
    public interface IAnalyticService : IAnalyticEventSender, IUserPropertySetter
    {
        bool IsInitialized { get; }

        UniTask Initialize();
    }
}