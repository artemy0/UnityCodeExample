using UnityEditor;
using UnityEngine;

namespace General.Extensions
{
    public static class GameObjectExtensions
    {
        public static T FindComponentInWholeObject<T>(this GameObject gameObject)
            where T : Component =>
            gameObject.GetComponent<T>() ??
            gameObject.GetComponentInChildren<T>() ??
            gameObject.GetComponentInParent<T>();

        public static T FindComponentInWholeObject<T>(this MonoBehaviour component)
            where T : Component =>
            component.gameObject.FindComponentInWholeObject<T>();

        public static void BakeComponentIfExist<T>(this MonoBehaviour monoBehaviour, ref T component)
            where T : Component
        {
            if (!monoBehaviour || component)
            {
                return;
            }

            component = monoBehaviour.FindComponentInWholeObject<T>();
#if UNITY_EDITOR
            if (component)
            {
                EditorUtility.SetDirty(component);
            }
#endif
        }
    }
}