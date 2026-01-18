using UI.General.Elements.Widget.Container;
using UnityEngine;

namespace UI.General.Elements.Widget
{
    [RequireComponent(typeof(UIWidgetRegistrator))]
    public abstract class UIWidget : UIElement
    {
        public virtual void Refresh()
        {
        }
    }
}