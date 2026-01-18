using System.Collections.Generic;
using System.Linq;
using General.Configs;
using Tools.PrepareBuild.Editor.Extensions;
using Tools.PrepareBuild.Editor.Steps.Abstraction;
using UnityEditor;
using UnityEditor.AddressableAssets;

namespace Tools.PrepareBuild.Editor.Steps
{
    public class CheckNecessaryAddressableLevelGroupsBuildStep : IPrepareBuildStep
    {
        private const string NAME = nameof(CheckNecessaryAddressableLevelGroupsBuildStep);

        public void PrepareBuild(bool showLogs)
        {
            var addressableSettings = AddressableAssetSettingsDefaultObject.Settings;
            var addressableGroups = addressableSettings.groups;

            var levelConfigsToReimport = new List<MockLevelConfig>();
            var levelConfigs = EditorLevelExtensions.GetLevelConfigs();

            var totalLevelConfigCount = levelConfigs.Count;
            for (var currentLevelConfigIndex = 0; currentLevelConfigIndex < totalLevelConfigCount; currentLevelConfigIndex++)
            {
                var levelConfig = levelConfigs[currentLevelConfigIndex];

                EditorUtility.DisplayProgressBar(NAME, levelConfig.name.ToLower(), (float) currentLevelConfigIndex / totalLevelConfigCount);

                if (!addressableGroups.Any(group => group.Name.Contains(levelConfig.Name)))
                {
                    levelConfigsToReimport.Add(levelConfig);
                }
            }

            if (levelConfigsToReimport.Count != 0)
            {
                EditorUtility.DisplayDialog(
                    $"Need reimport {levelConfigsToReimport.Count} levels",
                    $"Levels: {string.Join(", ", levelConfigsToReimport.Select(lc => lc.name))}",
                    "OK");
            }
        }
    }
}