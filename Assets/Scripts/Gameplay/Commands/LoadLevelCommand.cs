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
        public LoadLevelCommand()
        {
            // Setup dependencies
        }
        
        public UniTask ExecuteAsync(LoadLevelData commandData, CancellationToken disposeCtsToken)
        {
            var levelIndex = commandData.LevelIndex;
            
            // Unload ald level
            // Load new level
            
            return UniTask.CompletedTask;
        }
    }
}