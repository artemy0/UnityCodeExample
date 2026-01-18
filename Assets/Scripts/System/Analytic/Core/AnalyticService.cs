using System.Analytic.Core.Cache;
using System.Analytic.Events;
using System.Analytic.Modules;
using System.Analytic.PrivacyPolicy;
using System.Analytic.UserProperties;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Core
{
    public class AnalyticService : IAnalyticService, IDisposable
    {
        private readonly AnalyticEventsCache analyticEventsCache = new();
        private readonly UserPropertyCache userPropertiesCache = new();

        private readonly IEnumerable<IAnalyticModule> analyticModules;
        private readonly IEnumerable<IUserPropertyModule> userPropertyModules;
        private readonly IPrivacyPolicyService privacyPolicyService;

        private readonly CancellationTokenSource disposeCts = new();

        private CancellationToken cancellationToken;

        public bool IsInitialized { get; private set; }

        public AnalyticService(
            IEnumerable<IAnalyticModule> analyticModules,
            IEnumerable<IUserPropertyModule> userPropertyModules,
            IPrivacyPolicyService privacyPolicyService)
        {
            this.analyticModules = analyticModules;
            this.userPropertyModules = userPropertyModules;
            this.privacyPolicyService = privacyPolicyService;
        }

        public async UniTask Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            cancellationToken = disposeCts.Token;

            await privacyPolicyService.WaitForAcceptance(cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            foreach (var analyticService in analyticModules)
            {
                await analyticService.Initialize();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            foreach (var userPropertyModule in userPropertyModules)
            {
                await userPropertyModule.Initialize();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }

            IsInitialized = true;
            SendEventsFromCache();
        }

        public void Dispose()
        {
            disposeCts.Cancel();
            disposeCts.Dispose();
        }

        public void SendAnalyticEvent(IAnalyticEvent analyticEvent)
        {
            if (!IsInitialized)
            {
                CacheEvent(analyticEvent);
                return;
            }

            SendEvent(analyticEvent);
        }

        public void SetUserProperty(IUserProperty userProperty)
        {
            if (!IsInitialized)
            {
                CacheUserProperty(userProperty);
                return;
            }

            foreach (var module in userPropertyModules)
            {
                module.SetUserProperty(userProperty);
            }

            userProperty.Release();
        }

        private void SendEvent(IAnalyticEvent analyticEvent)
        {
            foreach (var module in analyticModules.Where(module => module.IsInitialized))
            {
                module.SendAnalyticEvent(analyticEvent);
            }

            analyticEvent.Release();
        }

        private void SendEventsFromCache()
        {
            SendUserPropertiesFromCache();

            while (analyticEventsCache.Next(out var analyticEvent))
            {
                SendAnalyticEvent(analyticEvent);
            }
        }

        private void CacheEvent(IAnalyticEvent analyticEvent)
        {
            analyticEventsCache.Append(analyticEvent);
        }

        private void CacheUserProperty(IUserProperty userProperty)
        {
            userPropertiesCache.Append(userProperty);
        }

        private void SendUserPropertiesFromCache()
        {
            while (userPropertiesCache.Next(out var userProperty))
            {
                SetUserProperty(userProperty);
            }
        }
    }
}