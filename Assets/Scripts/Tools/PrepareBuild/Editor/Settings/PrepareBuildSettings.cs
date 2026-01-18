using UnityEngine;

namespace Tools.PrepareBuild.Editor.Settings
{
    [CreateAssetMenu(
        menuName = "Setting/PrepareBuild/" + nameof(PrepareBuildSettings),
        fileName = nameof(PrepareBuildSettings))]
    public class PrepareBuildSettings : ScriptableObject
    {
        [SerializeField]
        private bool showLogs;

        public bool ShowLogs => showLogs;
    }
}