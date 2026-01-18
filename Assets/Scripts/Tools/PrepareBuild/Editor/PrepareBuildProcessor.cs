using System;
using System.Collections.Generic;
using Tools.PrepareBuild.Editor.Extensions;
using Tools.PrepareBuild.Editor.Settings;
using Tools.PrepareBuild.Editor.Steps;
using Tools.PrepareBuild.Editor.Steps.Abstraction;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Tools.PrepareBuild.Editor
{
    public class PrepareBuildProcessor
    {
        private static readonly List<IPrepareBuildStep> sortedPrepareBuildSteps = new()
        {
            // ...
            new CheckNecessaryAddressableLevelGroupsBuildStep(),
            // ...
        };

        public static void PrepareBuild()
        {
            try
            {
                var settings = EditorScriptableObjectExtensions.FindFirstScriptableObjectAsset<PrepareBuildSettings>();

                AssetDatabase.StartAssetEditing();

                foreach (var prepareBuildSteps in sortedPrepareBuildSteps)
                {
                    EditorUtility.DisplayProgressBar("PrepareBuild Processor", prepareBuildSteps.GetType().Name, 0f);
                    prepareBuildSteps.PrepareBuild(settings.ShowLogs);
                    EditorUtility.ClearProgressBar();
                }

                Debug.Log("PrepareBuild successfully");
            }
            catch (Exception ex)
            {
                Debug.Log($"PrepareBuild failed with message: {ex.Message}");
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                EditorUtility.ClearProgressBar();
            }
        }
    }
}