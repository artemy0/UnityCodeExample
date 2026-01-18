using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace General.Commands
{
    public interface ICommandExecutor
    {
        UniTask ExecuteAsync<T>()
            where T : IAsyncCommand;

        UniTask ExecuteAsync<TCommand, TData>(TData commandData)
            where TCommand : IAsyncCommand<TData>;
    }

    public abstract class AsyncCommandAsset<T> : ScriptableObject, IAsyncCommand<T>
    {
        public abstract UniTask ExecuteAsync(T commandData, CancellationToken cancellationToken);
    }

    public interface IAsyncCommand<in T> : ICommand
    {
        UniTask ExecuteAsync(T commandData, CancellationToken disposeCtsToken);
    }

    public interface IAsyncCommand : ICommand
    {
        UniTask ExecuteAsync(CancellationToken disposeCtsToken);
    }

    public interface ICommand
    {
    }
}