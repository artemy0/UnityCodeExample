using System.Collections.Generic;
using General.Mock;

namespace Tools.PrepareBuild.Editor.Extensions
{
    public static class EditorLevelExtensions
    {
        public static IReadOnlyList<MockLevelConfig> GetLevelConfigs()
        {
            return new List<MockLevelConfig>();
        }
    }
}