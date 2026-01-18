using UnityEngine;

namespace UI.General.Elements.Widget.Container
{
    [ExecuteAlways]
    public class UIWidgetRegistrator : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnEnable()
        {
            if (Application.isPlaying)
            {
                return;
            }

            RegisterInContainer();
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
            {
                return;
            }

            UnregisterFromRoot();
        }

        private void RegisterInContainer()
        {
            // ...
        }

        private void UnregisterFromRoot()
        {
            // ...
        }
#endif
    }
}