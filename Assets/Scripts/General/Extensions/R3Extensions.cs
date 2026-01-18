using System;
using R3;

namespace General.Extensions
{
    public static class R3Extensions
    {
        public static IDisposable SubscribeWithoutInitial<T>(this Observable<T> source, Action<T> onNext)
        {
            return source.Skip(1).Subscribe(onNext);
        }
    }
}