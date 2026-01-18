using Cysharp.Threading.Tasks;
using Unity.Notifications;

namespace System.LocalNotifications.Core
{
    public interface ILocalNotificationsManager
    {
        UniTask Initialize();

        void ScheduleNotification(
            Notification gameNotification,
            DateTime deliveryTime,
            NotificationRepeatInterval repeatInterval = NotificationRepeatInterval.OneTime);

        void ClearNotification(int notificationId);
    }
}