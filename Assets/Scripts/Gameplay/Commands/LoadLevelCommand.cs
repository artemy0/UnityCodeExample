using System.Threading;
using Cysharp.Threading.Tasks;
using General.Commands;

namespace Gameplay.Commands
{
    public struct LoadLevelData
    {
        public int LevelIndex;
    }

    public class LoadLevelCommand : IAsyncCommand<LoadLevelData>
    {
        private UniTask[] cacheTasks = new UniTask[2];

        public LoadLevelCommand()
        {
            // Setup dependencies
        }
        
        public async UniTask ExecuteAsync(LoadLevelData commandData, CancellationToken disposeCtsToken)
        {
            var levelIndex = commandData.LevelIndex;
            
            // Unload ald level

            cacheTasks[0] = UniTask.WaitForSeconds(1f, cancellationToken: disposeCtsToken);
            cacheTasks[1] = UniTask.WaitUntil(() => true, cancellationToken: disposeCtsToken);
            await UniTask.WhenAll(cacheTasks);
            
            // Load new level
        }
    }
}