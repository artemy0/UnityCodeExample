using UnityEditor;
using UnityEngine;

namespace Tools.PrepareBuild.Editor
{
    public class BuildPrepareWindow : EditorWindow
    {
        private const int DEFAULT_GUI_LAYOUT_SPACING = 20;
        
        [MenuItem("File/Build Prepare...", false, MenuItemPriorityOrder.FILE_BUILD_SETTINGS_PRIORITY)]
        public static void ShowBuildPrepareWindow()
        {
            GetWindow<BuildPrepareWindow>("BuildPrepare...");
        }

        private void OnGUI()
        {
            // ...
            
            GUILayout.Space(DEFAULT_GUI_LAYOUT_SPACING);

            GUILayout.Label("...", EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("PrepareBuild"))
            {
                PrepareBuildProcessor.PrepareBuild();
            }

            GUILayout.Space(DEFAULT_GUI_LAYOUT_SPACING);

            // ...
        }
    }
}