using UnityEngine;

namespace General.Configs
{
    [CreateAssetMenu(
        menuName = "Config/Level/" + nameof(MockLevelConfig),
        fileName = nameof(MockLevelConfig))]
    public class MockLevelConfig : ScriptableObject
    {
        public string Name => "..._Level";
    }
}