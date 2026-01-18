using Cysharp.Threading.Tasks;
using Unity.Notifications;
using UnityEngine;

namespace System.LocalNotifications.Core
{
    public class LocalNotificationsManager : ILocalNotificationsManager
    {
        private readonly LocalNotificationsSettings settings;

        public LocalNotificationsManager(LocalNotificationsSettings settings)
        {
            this.settings = settings;
        }

        public async UniTask Initialize()
        {
            var args = NotificationCenterArgs.Default;
            args.AndroidChannelId = settings.AndroidChannelId;
            args.AndroidChannelName = settings.AndroidChannelName;
            args.AndroidChannelDescription = settings.AndroidChannelDescription;

            NotificationCenter.Initialize(args);
            await NotificationCenter.RequestPermission();

            ClearAllNotifications();
        }

        public void ScheduleNotification(
            Notification gameNotification,
            DateTime deliveryTime,
            NotificationRepeatInterval repeatInterval = NotificationRepeatInterval.OneTime)
        {
            var schedule = new NotificationDateTimeSchedule(deliveryTime, repeatInterval);

            NotificationCenter.ScheduleNotification(
                gameNotification,
                schedule);

            Log(gameNotification, schedule);
        }

        public void ClearNotification(int notificationId)
        {
            NotificationCenter.CancelScheduledNotification(notificationId);
            NotificationCenter.CancelDeliveredNotification(notificationId);
        }

        private void ClearAllNotifications()
        {
            NotificationCenter.CancelAllDeliveredNotifications();
            NotificationCenter.CancelAllScheduledNotifications();
        }

        private void Log(Notification gameNotification, NotificationDateTimeSchedule schedule)
        {
            if (!settings.ShowLogs)
            {
                return;
            }

            var title = string.IsNullOrEmpty(gameNotification.Title) ? "NoTitle" : gameNotification.Title;
            var text = string.IsNullOrEmpty(gameNotification.Text) ? "NoText" : gameNotification.Text;

            Debug.Log($"[Notification] {title}:{text} should appear at {schedule.FireTime} in {schedule.RepeatInterval} mode");
        }
    }
}