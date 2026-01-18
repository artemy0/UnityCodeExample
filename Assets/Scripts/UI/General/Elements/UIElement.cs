using System;
using Cysharp.Threading.Tasks;
using TriInspector;
using UI.General.Animation;
using UI.General.Elements.Base;
using UnityEngine;

namespace UI.General.Elements
{
    [DeclareFoldoutGroup(ANIMATION_GROUP)]
    public abstract class UIElement : BaseViewModel
    {
        private const string ANIMATION_GROUP = "Animations";
        
        public event Action OnShowStartedEvent;
        public event Action OnShowCompletedEvent;
        public event Action OnHideStartedEvent;
        public event Action OnHideCompletedEvent;

        [SerializeField] [Group(ANIMATION_GROUP)]
        private UIAnimation showAnimation;

        [SerializeField] [Group(ANIMATION_GROUP)]
        private UIAnimation hideAnimation;

        public bool IsActive { get; private set; }

        public bool IsPlayingShowAnimation => showAnimation != null && showAnimation.IsPlaying;
        public bool IsPlayingHideAnimation => hideAnimation != null && hideAnimation.IsPlaying;

        public UIAnimation ShowAnimation => showAnimation;
        public UIAnimation HideAnimation => hideAnimation;

        public async UniTask Show(bool force = false)
        {
            IsActive = true;

            OnShowStartedEvent?.Invoke();
            OnShowStarted();

            if (showAnimation)
            {
                if (!force)
                {
                    await showAnimation.PlayAsync();
                    if (destroyCancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
                else
                {
                    showAnimation.CompleteAnimation();
                }
            }

            OnShowCompletedEvent?.Invoke();
            OnShowComplete();
        }

        public async UniTask Hide(bool force = false)
        {
            IsActive = false;

            OnHideStartedEvent?.Invoke();
            OnHideStarted();

            if (hideAnimation)
            {
                if (!force)
                {
                    await hideAnimation.PlayAsync();
                    if (destroyCancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }
                else
                {
                    hideAnimation.CompleteAnimation();
                }
            }

            OnHideCompletedEvent?.Invoke();
            OnHideComplete();
        }

        public async UniTask WaitForClose()
        {
            while (IsActive || IsPlayingHideAnimation)
            {
                await UniTask.Yield();
                if (destroyCancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public async UniTask WaitForShow()
        {
            while (!IsActive || IsPlayingShowAnimation)
            {
                await UniTask.Yield();
                if (destroyCancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        protected virtual void OnShowStarted()
        {
        }

        protected virtual void OnShowComplete()
        {
        }

        protected virtual void OnHideStarted()
        {
        }

        protected virtual void OnHideComplete()
        {
        }

        private void Reset()
        {
            var show = transform.Find("Animations/Show");
            var hide = transform.Find("Animations/Hide");

            if (show)
            {
                showAnimation = show.GetComponent<UIAnimation>();
            }

            if (hide)
            {
                hideAnimation = hide.GetComponent<UIAnimation>();
            }
        }
    }
}