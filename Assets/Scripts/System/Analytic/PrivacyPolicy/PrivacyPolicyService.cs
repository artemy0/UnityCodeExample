using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace System.Analytic.PrivacyPolicy
{
    public class PrivacyPolicyService : IPrivacyPolicyService
    {
        private const string PRIVACY_POLICY_ACCEPTED_KEY = "PrivacyPolicyAcepted";

        public bool IsAccepted => PlayerPrefs.GetInt(PRIVACY_POLICY_ACCEPTED_KEY) == 1;

        public UniTask WaitForAcceptance(CancellationToken cancellationToken = default) =>
            IsAccepted ? UniTask.CompletedTask : UniTask.WaitUntil(() => IsAccepted, cancellationToken: cancellationToken);

        public void Accept() => PlayerPrefs.SetInt(PRIVACY_POLICY_ACCEPTED_KEY, 1);
    }
}