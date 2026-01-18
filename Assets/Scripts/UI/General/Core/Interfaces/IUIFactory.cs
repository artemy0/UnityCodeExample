using Cysharp.Threading.Tasks;
using UI.General.Elements.Popup;
using UI.General.Elements.Screen;
using UI.General.Elements.Widget;
using UnityEngine;

namespace UI.General.Core.Interfaces
{
    public interface IUIFactory
    {
        UniTask<TScreen> CreateScreenAsync<TScreen>(Transform parent)
            where TScreen : UIScreen;

        TScreen CreateScreen<TScreen>(Transform parent)
            where TScreen : UIScreen;

        UniTask<UIPopup> CreatePopupAsync<TPopup>(Transform parent)
            where TPopup : UIPopup;

        UIPopup CreatePopup<TPopup>(Transform parent)
            where TPopup : UIPopup;

        UniTask<TWidget> CreateWidgetAsync<TWidget>(Transform container)
            where TWidget : UIWidget;

        void ReleaseWidget<TWidget>(TWidget widget)
            where TWidget : UIWidget;
    }
}