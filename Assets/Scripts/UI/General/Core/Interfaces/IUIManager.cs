using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI.General.Elements.Popup;
using UI.General.Elements.Screen;
using UI.General.Elements.Widget;

namespace UI.General.Core.Interfaces
{
    public interface IUIManager : IDisposable
    {
        event Action<UIScreen> OnScreenShown;
        event Action<UIScreen> OnScreenHidden;

        event Action<UIPopup> OnPopupShown;
        event Action<UIPopup> OnPopupHidden;

        int ActivePopupsCount { get; }
        UIScreen ActiveScreen { get; }

        // Warmup
        UniTask<TScreen> WarmupScreenAsync<TScreen>(int? sortOrder = null)
            where TScreen : UIScreen;

        UniTask<TPopup> WarmupPopupAsync<TPopup>(int? sortOrder = null)
            where TPopup : UIPopup;

        // Get
        bool TryGetScreen<T>(out T result)
            where T : UIScreen;

        bool TryGetPopup<T>(out T result)
            where T : UIPopup;

        // Show
        UniTask<TScreen> ShowScreenAsync<TScreen>(int? sortOrder = null)
            where TScreen : UIScreen;

        UniTask<TScreen> ShowScreenAsync<TScreen, TData>(TData screenData, int? sorterOrder = null)
            where TScreen : UIScreenT<TData>;

        UniTask<TPopup> ShowPopupAsync<TPopup>(int? sortOrder = null)
            where TPopup : UIPopup;

        UniTask<TPopup> ShowPopupAsync<TPopup, TData>(TData popupData, int? sortOrder = null)
            where TPopup : UIPopupT<TData>;

        // Wait
        UniTask<TScreen> WaitForScreenCreatedAsync<TScreen>(CancellationToken disposeToken)
            where TScreen : UIScreen;

        UniTask<TPopup> WaitForPopupCreatedAsync<TPopup>(CancellationToken disposeToken)
            where TPopup : UIPopup;

        // Hide
        UniTask<TScreen> HideScreenAsync<TScreen>()
            where TScreen : UIScreen;

        UniTask<UIScreen> HideScreenAsync(Type screenType);

        UniTask<TPopup> HidePopupAsync<TPopup>()
            where TPopup : UIPopup;

        UniTask<UIPopup> HidePopupAsync(Type popupType);

        // Widget
        bool TryGetWidgetFromScreen<TScreen, TWidget>(out TWidget result)
            where TScreen : UIScreen
            where TWidget : UIWidget;

        UniTask<TWidget> AddWidgetToScreenAsync<TScreen, TWidget>()
            where TScreen : UIScreen
            where TWidget : UIWidget;

        void RemoveWidgetFromScreenAsync<TScreen, TWidget>()
            where TScreen : UIScreen
            where TWidget : UIWidget;

        bool TryGetWidgetFromPopup<TPopup, TWidget>(out TWidget result)
            where TPopup : UIPopup
            where TWidget : UIWidget;
    }
}