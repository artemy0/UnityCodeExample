using UnityEngine.Pool;

namespace System.Analytic.UserProperties
{
    public static class UserPropertiesPool<T>
        where T : class, IUserProperty, new()
    {
        private static readonly ObjectPool<T> pool = new(OnCreate);

        public static T Get() => pool.Get();

        public static PooledObject<T> Get(out T value) => pool.Get(out value);

        public static void Release(T toRelease)
        {
            pool.Release(toRelease);
        }

        private static T OnCreate() => new();
    }
}