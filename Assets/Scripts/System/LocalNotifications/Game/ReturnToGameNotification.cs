using System.LocalNotifications.Core;
using Unity.Notifications;

namespace System.LocalNotifications.Game
{
    public class ReturnToGameNotification : ApplicationUnfocusNotification
    {
        private readonly LocalNotificationsSettings settings;

        protected override string NotificationTitle => string.Empty;
        protected override string NotificationBody => "Come back ...!";

        protected override int NotificationId => NotificationIDs.RETURN_TO_GAME_25_H;
        protected override int NotificationDelaySeconds => settings.ReturnToGameNotificationDelaySeconds;
        protected override NotificationRepeatInterval NotificationRepeatInterval => NotificationRepeatInterval.Daily;

        public ReturnToGameNotification(
            ILocalNotificationsManager notificationsManager,
            LocalNotificationsSettings settings) : base(notificationsManager)
        {
            this.settings = settings;
        }
    }
}