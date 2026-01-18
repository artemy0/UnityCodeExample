using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        menuName = "Config/" + nameof(MockGlobalConfig),
        fileName = nameof(MockGlobalConfig))]
    public class MockGlobalConfig : ScriptableObject
    {
        public int MaxLevelIndex;
    }
}