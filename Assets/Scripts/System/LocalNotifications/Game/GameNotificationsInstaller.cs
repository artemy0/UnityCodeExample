using General.Injection;
using VContainer;
using VContainer.Unity;

namespace System.LocalNotifications.Game
{
    public class GameNotificationsInstaller : MonoInstaller
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ReturnToGameNotification>();
        }
    }
}