using General.Injection;
using UnityEngine;
using VContainer;

namespace System.Analytic.Analytics.Firebase
{
    public class FirebaseInstaller : MonoInstaller
    {
        [SerializeField]
        private FirebaseModulesSettings firebaseSettings;

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(firebaseSettings);
            /*
            builder.CreateAnalyticModuleBuilder<FirebaseAnalyticModule>()
                .AddUserPropertyModule<FirebaseUserPropertyModule>()
                .AddEventFilter<FirebaseEventFilter>()
                .Build();
            */
        }
    }
}