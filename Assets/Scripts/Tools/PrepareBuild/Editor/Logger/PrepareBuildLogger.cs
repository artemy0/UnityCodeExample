using UnityEngine;

namespace Tools.PrepareBuild.Editor.Logger
{
    public class PrepareBuildLogger
    {
        private readonly bool showLogs;

        public PrepareBuildLogger(bool showLogs)
        {
            this.showLogs = showLogs;
        }

        public void LogIfNecessary(Object changedObject)
        {
            if (!showLogs)
            {
                return;
            }

            Debug.Log($"[Build Prepare] {changedObject.name} asset changed", changedObject);
        }
    }
}