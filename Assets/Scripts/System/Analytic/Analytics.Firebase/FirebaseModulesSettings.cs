using UnityEngine;

namespace System.Analytic.Analytics.Firebase
{
    [CreateAssetMenu(
        fileName = nameof(FirebaseModulesSettings),
        menuName = "Analytics/Settings/" + nameof(FirebaseModulesSettings))]
    public class FirebaseModulesSettings : ScriptableObject
    {
        public bool EnableEventDebug;
        public bool EnableUserPropertyDebug;
    }
}