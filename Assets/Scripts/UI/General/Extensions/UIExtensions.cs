using UI.General.Elements;
using UnityEngine;

namespace UI.General.Extensions
{
    public static class UIExtensions
    {
        public static void SetFullScreen(this UIElement uiElement, Transform parent = null)
        {
            var rectTransform = uiElement.GetComponent<RectTransform>();

            rectTransform.SetFullScreen(parent);
        }

        public static void SetFullScreen(this RectTransform rectTransform, Transform parent = null)
        {
            rectTransform.SetParent(parent ? parent : rectTransform.parent, false);

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.sizeDelta = Vector2.zero;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            rectTransform.localScale = Vector3.one;
        }
    }
}