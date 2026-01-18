using UnityEngine;

namespace System.LocalNotifications
{
    [Serializable]
    public class LocalNotificationsSettings
    {
        [Header("Base settings")]
        public bool ShowLogs;

        [Header("Android settings")]
        public string AndroidChannelId = "...";
        public string AndroidChannelName = "...";
        public string AndroidChannelDescription = "...";

        [Header("Notification settings")]
        public int ReturnToGameNotificationDelaySeconds = 60 * 25;
    }
}