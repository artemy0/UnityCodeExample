using System.Collections.Generic;
using General.Configs;

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