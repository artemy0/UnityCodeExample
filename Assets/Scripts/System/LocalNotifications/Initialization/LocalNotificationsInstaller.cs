using System.LocalNotifications.Core;
using General.Injection;
using UnityEngine;
using VContainer;

namespace System.LocalNotifications.Initialization
{
    public class LocalNotificationsInstaller : MonoInstaller
    {
        [SerializeField]
        private LocalNotificationsSettings settings;

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(settings);
            builder.Register<LocalNotificationsManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LocalNotificationsInitializeStep>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}