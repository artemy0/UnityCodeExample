using System.LocalNotifications.Core;
using Unity.Notifications;
using UnityEngine;
using VContainer.Unity;

namespace System.LocalNotifications.Game
{
    public abstract class ApplicationUnfocusNotification : IStartable, IDisposable
    {
        private readonly ILocalNotificationsManager notificationsManager;

        protected abstract string NotificationTitle { get; }
        protected abstract string NotificationBody { get; }
        protected abstract int NotificationId { get; }
        protected abstract int NotificationDelaySeconds { get; }
        protected abstract NotificationRepeatInterval NotificationRepeatInterval { get; }

        protected ApplicationUnfocusNotification(ILocalNotificationsManager notificationsManager)
        {
            this.notificationsManager = notificationsManager;
        }

        public void Start()
        {
            Application.focusChanged += OnApplicationFocus;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnApplicationFocus;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                var notification = new Notification
                {
                    Identifier = NotificationId,
                    Title = NotificationTitle,
                    Text = NotificationBody,
                };

                var deliveryTime = DateTime.Now.AddSeconds(NotificationDelaySeconds);
                notificationsManager.ScheduleNotification(notification, deliveryTime, NotificationRepeatInterval);
            }
            else
            {
                notificationsManager.ClearNotification(NotificationId);
            }
        }
    }
}