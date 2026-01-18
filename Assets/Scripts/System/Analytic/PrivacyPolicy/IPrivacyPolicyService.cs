using System.Threading;
using Cysharp.Threading.Tasks;

namespace System.Analytic.PrivacyPolicy
{
    public interface IPrivacyPolicyService
    {
        bool IsAccepted { get; }

        UniTask WaitForAcceptance(CancellationToken cancellationToken = default);
        void Accept();
    }
}