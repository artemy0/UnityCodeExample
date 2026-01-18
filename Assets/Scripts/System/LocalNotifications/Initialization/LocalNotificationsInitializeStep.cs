using System.LocalNotifications.Core;
using Cysharp.Threading.Tasks;
using General.Initialization;

namespace System.LocalNotifications.Initialization
{
    public class LocalNotificationsInitializeStep : IApplicationInitializationStep
    {
        private readonly ILocalNotificationsManager localNotificationsManager;

        public string Name => "Local Notifications";
        public float Progress { get; private set; }

        public LocalNotificationsInitializeStep(ILocalNotificationsManager localNotificationsManager)
        {
            this.localNotificationsManager = localNotificationsManager;
        }

        public async UniTask InitializeAsync()
        {
            Progress = 0;
            await localNotificationsManager.Initialize();
            Progress = 1;
        }
    }
}