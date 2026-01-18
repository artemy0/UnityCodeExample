using UnityEngine.Pool;

namespace System.Analytic.Events
{
    public static class EventsPool<T>
        where T : class, IAnalyticEvent, new()
    {
        private static readonly ObjectPool<T> pool = new(OnCreate);

        public static T Get() => pool.Get();

        public static PooledObject<T> Get(out T value) => pool.Get(out value);

        public static void Release(T toRelease)
        {
            toRelease.Dispose();
            pool.Release(toRelease);
        }

        private static T OnCreate() => new();
    }
}