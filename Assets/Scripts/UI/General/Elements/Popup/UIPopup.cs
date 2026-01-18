using Cysharp.Threading.Tasks;
using General.Extensions;
using TriInspector;
using UI.General.Core.Interfaces;
using UI.General.Elements.Widget.Container;
using UI.General.Sort;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General.Elements.Popup
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [DeclareFoldoutGroup(HIDE_GROUP)]
    [DeclareFoldoutGroup(OPTIONAL_GROUP)]
    public abstract class UIPopup : UIElement, IUIWidgetContainer, IUIElementRoot
    {
        private const string OPTIONAL_GROUP = "Optional settings";
        private const string HIDE_GROUP = "Hide settings";

        [SerializeField] [Group(OPTIONAL_GROUP)] [PropertyOrder(70)]
        private Button closeButton;

        [SerializeField] [Group(HIDE_GROUP)] [PropertyOrder(90)]
        private bool autoHideOnTapAnywhere;

        [SerializeField] [Group(HIDE_GROUP)] [PropertyOrder(91)]
        private bool enableAutoHide;

        [SerializeField] [Group(HIDE_GROUP)] [PropertyOrder(92)] [ShowIf(nameof(enableAutoHide))] [Min(0)]
        private float autoHideDelay = 3f;

        [SerializeField] [HideInInspector]
        private Canvas targetCanvas;

        [SerializeField] [HideInInspector]
        private CanvasGroup targetCanvasGroup;

        [SerializeField] [HideInInspector]
        private WidgetsContainer targetWidgetsContainer;

        public WidgetsContainer RuntimeWidgetsContainer => targetWidgetsContainer;
        protected IUIManager UIManager { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.BakeComponentIfExist(ref targetCanvas);
            this.BakeComponentIfExist(ref targetCanvasGroup);
            this.BakeComponentIfExist(ref targetWidgetsContainer);
        }
#endif

        private void Awake()
        {
            if (closeButton)
            {
                closeButton.onClick.AddListener(Close);
            }
        }

        private void OnDestroy()
        {
            if (closeButton)
            {
                closeButton.onClick.RemoveListener(Close);
            }
        }

        public void SetDependencies(IUIManager uiManager)
        {
            UIManager = uiManager;
        }

        protected override void OnShowStarted()
        {
            targetCanvas.enabled = true;
            targetCanvasGroup.interactable = false;
            targetWidgetsContainer.RefreshWidgets();
            base.OnShowStarted();

            if (autoHideOnTapAnywhere)
            {
                HideSelfIfTapAnywhere().Forget();
            }

            if (enableAutoHide && autoHideDelay > 0)
            {
                HideSelfAfterDelay().Forget();
            }
        }

        protected override void OnShowComplete()
        {
            base.OnShowComplete();
            targetCanvasGroup.interactable = true;
        }

        protected override void OnHideStarted()
        {
            targetCanvasGroup.interactable = false;
            base.OnHideStarted();
        }

        protected override void OnHideComplete()
        {
            base.OnHideComplete();
            targetCanvas.enabled = false;
        }

        private async UniTaskVoid HideSelfIfTapAnywhere()
        {
            while (IsActive)
            {
                if (destroyCancellationToken.IsCancellationRequested)
                {
                    return;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Close();
                    return;
                }

                await UniTask.Yield();
            }
        }

        private async UniTaskVoid HideSelfAfterDelay()
        {
            var timer = autoHideDelay;
            while (timer > 0 && IsActive)
            {
                if (destroyCancellationToken.IsCancellationRequested)
                {
                    return;
                }

                timer -= Time.deltaTime;
                await UniTask.Yield();
            }

            Close();
        }

        protected void Close()
        {
            if (IsActive)
            {
                var selfType = GetType();
                UIManager.HidePopupAsync(selfType);
            }
        }
    }
}