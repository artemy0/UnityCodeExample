using System.Collections;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace UI.General.Elements.Widget.Container
{
    [DisallowMultipleComponent]
    public class WidgetsContainer : MonoBehaviour, IEnumerable<UIWidget>
    {
        [SerializeField] [ReadOnly]
        private List<UIWidget> widgets = new();

        private RectTransform runtimeWidgetsContainer;

        internal RectTransform RuntimeWidgetsParent
        {
            get
            {
                if (!runtimeWidgetsContainer)
                {
                    runtimeWidgetsContainer = CreateContainerForRuntimeWidgets();
                }

                return runtimeWidgetsContainer;
            }
        }

        public IEnumerator<UIWidget> GetEnumerator() => widgets.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal bool ContainsWidget<TWidget>()
            where TWidget : UIWidget
        {
            // ...

            return false;
        }

        internal T FindWidget<T>()
            where T : UIWidget
        {
            // ...

            return null;
        }

        internal void RegisterWidget(UIWidget widget)
        {
            // ...
        }

        internal void UnregisterWidget(UIWidget widget)
        {
            // ...
        }

        internal TWidget RemoveWidget<TWidget>()
            where TWidget : UIWidget
        {
            // ...

            return null;
        }

        internal void RefreshWidgets()
        {
            // ...
        }

        private void Reset()
        {
            RegisterExistedWidgets();
        }

        private void RegisterExistedWidgets()
        {
            GetComponentsInChildren(widgets);
        }

        private RectTransform CreateContainerForRuntimeWidgets()
        {
            // ...

            return null;
        }
    }
}