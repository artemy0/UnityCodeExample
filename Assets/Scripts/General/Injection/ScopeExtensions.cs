using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace General.Injection
{
    public static class ScopeExtensions
    {
        public static void RegisterMonoInstallers(this LifetimeScope scope, IContainerBuilder builder)
        {
            foreach (var monoInstaller in scope.GetComponentsInChildren<MonoInstaller>())
            {
                monoInstaller.Configure(builder);
            }
        }
    }

    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}