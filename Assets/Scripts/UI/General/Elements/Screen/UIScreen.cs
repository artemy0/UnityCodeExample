using General.Extensions;
using UI.General.Elements.Widget.Container;
using UI.General.Sort;
using UnityEngine;

namespace UI.General.Elements.Screen
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(WidgetsContainer))]
    public abstract class UIScreen : UIElement, IUIWidgetContainer, IUIElementRoot
    {
        [SerializeField] [HideInInspector]
        private Canvas targetCanvas;

        [SerializeField] [HideInInspector]
        private WidgetsContainer targetWidgetsContainer;

        public WidgetsContainer RuntimeWidgetsContainer => targetWidgetsContainer;

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.BakeComponentIfExist(ref targetCanvas);
            this.BakeComponentIfExist(ref targetWidgetsContainer);
        }
#endif

        protected override void OnShowStarted()
        {
            targetCanvas.enabled = true;
            targetWidgetsContainer.RefreshWidgets();

            foreach (var widgetContainer in RuntimeWidgetsContainer)
            {
                widgetContainer.Show();
            }

            base.OnShowStarted();
        }

        protected override void OnHideComplete()
        {
            base.OnHideComplete();
            targetCanvas.enabled = false;
        }
    }
}