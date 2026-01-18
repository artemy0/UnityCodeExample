using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI.General.Core.Interfaces;
using UI.General.Elements.Popup;
using UI.General.Elements.Root;
using UI.General.Elements.Screen;
using UI.General.Elements.Widget;
using UI.General.Elements.Widget.Container;
using UI.General.Extensions;
using UI.General.Sort;
using UnityEngine;

namespace UI.General.Core
{
    public class UIManager : IUIManager
    {
        private const int DEFAULT_SORT_ORDER = 1000;

        public event Action<UIScreen> OnScreenShown;
        public event Action<UIScreen> OnScreenHidden;

        public event Action<UIPopup> OnPopupShown;
        public event Action<UIPopup> OnPopupHidden;

        private readonly Dictionary<Type, UIScreen> createdScreens = new();
        private readonly Dictionary<Type, UIPopup> createdPopups = new();
        private readonly Dictionary<Type, UIPopup> activePopups = new();

        private readonly CancellationTokenSource disposeCts = new();

        private readonly IUIFactory uiFactory;
        private readonly UIRoot uiRoot;

        public UIScreen ActiveScreen { get; private set; }
        public int ActivePopupsCount => activePopups.Count;

        public UIManager(
            IUIFactory uiFactory,
            UIRoot uiRoot)
        {
            this.uiFactory = uiFactory;
            this.uiRoot = uiRoot;
        }

        public void Dispose()
        {
            disposeCts.Cancel();
        }

        // Warmup
        public async UniTask<TScreen> WarmupScreenAsync<TScreen>(int? sortOrder = null)
            where TScreen : UIScreen
        {
            var screen = await GetOrCreateScreenAsync<TScreen>(sortOrder);

            await screen.Hide(force: true);
            return screen;
        }

        public async UniTask<TPopup> WarmupPopupAsync<TPopup>(int? sortOrder = null)
            where TPopup : UIPopup
        {
            var popup = await GetOrCreatePopupAsync<TPopup>(sortOrder);

            await popup.Hide(force: true);
            return popup;
        }

        // Get
        public bool TryGetScreen<T>(out T result)
            where T : UIScreen
        {
            if (createdScreens.TryGetValue(typeof(T), out var screen))
            {
                result = screen as T;
                return true;
            }

            result = null;
            return false;
        }

        public bool TryGetPopup<T>(out T result)
            where T : UIPopup
        {
            if (createdPopups.TryGetValue(typeof(T), out var popup))
            {
                result = popup as T;
                return true;
            }

            result = null;
            return false;
        }

        // Show
        public async UniTask<TScreen> ShowScreenAsync<TScreen>(int? sorterOrder = null)
            where TScreen : UIScreen
        {
            var screenToShow = await GetOrCreateScreenAsync<TScreen>(sorterOrder);

            if (ActiveScreen != null)
            {
                await ActiveScreen.Hide();
                OnScreenHidden?.Invoke(ActiveScreen);
            }

            ActiveScreen = screenToShow;

            await ActiveScreen.Show();
            OnScreenShown?.Invoke(ActiveScreen);

            return ActiveScreen as TScreen;
        }

        public async UniTask<TScreen> ShowScreenAsync<TScreen, TData>(TData screenData, int? sorterOrder = null)
            where TScreen : UIScreenT<TData>
        {
            var screenToShow = await GetOrCreateScreenAsync<TScreen>(sorterOrder);

            if (ActiveScreen != null)
            {
                await ActiveScreen.Hide();
                OnScreenHidden?.Invoke(ActiveScreen);
            }

            ActiveScreen = screenToShow;
            screenToShow.Initialize(screenData);

            await ActiveScreen.Show();
            OnScreenShown?.Invoke(ActiveScreen);

            return ActiveScreen as TScreen;
        }

        public async UniTask<TPopup> ShowPopupAsync<TPopup>(int? sortOrder = null)
            where TPopup : UIPopup
        {
            var popupType = typeof(TPopup);
            var popupToShow = await GetOrCreatePopupAsync<TPopup>(sortOrder);

            activePopups.Add(popupType, popupToShow);

            await popupToShow.Show();
            OnPopupShown?.Invoke(popupToShow);

            return popupToShow;
        }

        public async UniTask<TPopup> ShowPopupAsync<TPopup, TData>(TData popupData, int? sortOrder = null)
            where TPopup : UIPopupT<TData>
        {
            var popupType = typeof(TPopup);
            var popupToShow = await GetOrCreatePopupAsync<TPopup>(sortOrder);

            activePopups.Add(popupType, popupToShow);
            popupToShow.Initialize(popupData);

            await popupToShow.Show();
            OnPopupShown?.Invoke(popupToShow);

            return popupToShow;
        }

        // Wait
        public async UniTask<TScreen> WaitForScreenCreatedAsync<TScreen>(CancellationToken disposeToken)
            where TScreen : UIScreen
        {
            var screenType = typeof(TScreen);

            while (!createdScreens.ContainsKey(screenType))
            {
                await UniTask.Yield();
                if (disposeCts.IsCancellationRequested || disposeToken.IsCancellationRequested)
                {
                    return null;
                }
            }

            var shownScreen = (TScreen)createdScreens[screenType];
            return shownScreen;
        }

        public async UniTask<TPopup> WaitForPopupCreatedAsync<TPopup>(CancellationToken disposeToken)
            where TPopup : UIPopup
        {
            var popupType = typeof(TPopup);

            while (!createdPopups.ContainsKey(popupType))
            {
                await UniTask.Yield();
                if (disposeCts.IsCancellationRequested || disposeToken.IsCancellationRequested)
                {
                    return null;
                }
            }

            var shownPopup = (TPopup)createdPopups[popupType];
            return shownPopup;
        }

        // Hide
        public async UniTask<TScreen> HideScreenAsync<TScreen>()
            where TScreen : UIScreen
        {
            var screenType = typeof(TScreen);
            var screen = await HideScreenAsync(screenType);
            return screen as TScreen;
        }

        public async UniTask<UIScreen> HideScreenAsync(Type screenType)
        {
            var screenToHide = ActiveScreen;

            var isScreenActive = screenToHide != null && screenToHide.GetType() == screenType;
            if (isScreenActive)
            {
                ActiveScreen = null;

                await screenToHide.Hide();
                OnScreenHidden?.Invoke(screenToHide);
            }

            return screenToHide;
        }

        public async UniTask<TPopup> HidePopupAsync<TPopup>()
            where TPopup : UIPopup
        {
            var popupType = typeof(TPopup);
            var popup = await HidePopupAsync(popupType);
            return popup as TPopup;
        }

        public async UniTask<UIPopup> HidePopupAsync(Type popupType)
        {
            var isPopupActive = activePopups.TryGetValue(popupType, out var popupToHide);
            if (isPopupActive)
            {
                activePopups.Remove(popupType);

                await popupToHide.Hide();
                OnPopupHidden?.Invoke(popupToHide);
            }

            return popupToHide;
        }

        // Widget
        public bool TryGetWidgetFromScreen<TScreen, TWidget>(out TWidget result)
            where TScreen : UIScreen
            where TWidget : UIWidget
        {
            var screenType = typeof(TScreen);

            if (!createdScreens.TryGetValue(screenType, out var screen))
            {
                throw new Exception(
                    $"Screen with type {screenType.Name} not found. Please create it first.");
            }

            return TryGetWidgetFromContainer(screen, out result);
        }

        public UniTask<TWidget> AddWidgetToScreenAsync<TScreen, TWidget>()
            where TScreen : UIScreen
            where TWidget : UIWidget
        {
            var screenType = typeof(TScreen);

            if (!createdScreens.TryGetValue(screenType, out var screen))
            {
                throw new Exception(
                    $"Screen with type {screenType.Name} not found. Please create it first.");
            }

            return AddWidgetToContainer<TWidget>(screen);
        }

        public void RemoveWidgetFromScreenAsync<TScreen, TWidget>()
            where TScreen : UIScreen
            where TWidget : UIWidget
        {
            var screenType = typeof(TScreen);

            if (!createdScreens.TryGetValue(screenType, out var screen))
            {
                throw new Exception(
                    $"Screen with type {screenType.Name} not found. Please create it first.");
            }

            RemoveWidgetFromContainer<TWidget>(screen);
        }

        public bool TryGetWidgetFromPopup<TPopup, TWidget>(out TWidget result)
            where TPopup : UIPopup
            where TWidget : UIWidget
        {
            var popupType = typeof(TPopup);

            if (!createdPopups.TryGetValue(popupType, out var popup))
            {
                throw new Exception(
                    $"Popup with type {popupType.Name} not found. Please create it first.");
            }

            return TryGetWidgetFromContainer(popup, out result);
        }

        // Private
        private async UniTask<TScreen> GetOrCreateScreenAsync<TScreen>(int? sorterOrder = null)
            where TScreen : UIScreen
        {
            var screenType = typeof(TScreen);

            var isScreenActive = ActiveScreen != null && ActiveScreen.GetType() == screenType;
            if (isScreenActive)
            {
                return ActiveScreen as TScreen;
            }

            var isScreenCreated = createdScreens.TryGetValue(screenType, out var screen);
            if (!isScreenCreated)
            {
                screen = await uiFactory.CreateScreenAsync<TScreen>(uiRoot.Canvas.transform);

                SetUIUnitParent(screen.transform, sorterOrder, true);
                screen.SetFullScreen();

                createdScreens.Add(screenType, screen);
            }

            return screen as TScreen;
        }

        private async UniTask<TPopup> GetOrCreatePopupAsync<TPopup>(int? sorterOrder = null)
            where TPopup : UIPopup
        {
            var popupType = typeof(TPopup);
            if (activePopups.TryGetValue(popupType, out var activePopup))
            {
                return activePopup as TPopup;
            }

            var isPopupCreated = createdPopups.TryGetValue(popupType, out var popup);
            if (!isPopupCreated)
            {
                popup = await uiFactory.CreatePopupAsync<TPopup>(uiRoot.Canvas.transform);
                popup.SetDependencies(this);

                SetUIUnitParent(popup.transform, sorterOrder);
                popup.SetFullScreen();

                createdPopups.Add(popupType, popup);
            }

            return popup as TPopup;
        }

        private void SetUIUnitParent(Transform panelTransform, int? sorterOrder, bool safeArea = false)
        {
            sorterOrder ??= panelTransform.TryGetComponent(out SortableUIUnit uiUnitSortable)
                ? uiUnitSortable.SortOrder
                : DEFAULT_SORT_ORDER;

            var parentTransform = uiRoot.GetParent(sorterOrder.Value);
            panelTransform.SetParent(parentTransform);
        }

        private bool TryGetWidgetFromContainer<TWidget>(IUIWidgetContainer uiElement, out TWidget result)
            where TWidget : UIWidget
        {
            var widgetsContainer = uiElement.RuntimeWidgetsContainer;
            result = widgetsContainer.FindWidget<TWidget>();
            return result != null;
        }

        private async UniTask<TWidget> AddWidgetToContainer<TWidget>(IUIWidgetContainer uiElement)
            where TWidget : UIWidget
        {
            if (TryGetWidgetFromContainer<TWidget>(uiElement, out var widget))
            {
                return widget;
            }

            var widgetsContainer = uiElement.RuntimeWidgetsContainer;
            widget = await uiFactory.CreateWidgetAsync<TWidget>(widgetsContainer.RuntimeWidgetsParent);
            widgetsContainer.RegisterWidget(widget);
            return widget;
        }

        private void RemoveWidgetFromContainer<TWidget>(IUIWidgetContainer uiElement)
            where TWidget : UIWidget
        {
            var widgetsContainer = uiElement.RuntimeWidgetsContainer;
            var widget = widgetsContainer.RemoveWidget<TWidget>();
            if (widget)
            {
                uiFactory.ReleaseWidget(widget);
            }
        }
    }
}