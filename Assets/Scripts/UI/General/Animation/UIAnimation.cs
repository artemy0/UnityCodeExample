using System;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UI.General.Animation
{
    [DeclareBoxGroup(ANIMATION_GROUP, HideTitle = true)]
    [DeclareFoldoutGroup(EVENTS_GROUP)]
    public abstract class UIAnimation : MonoBehaviour
    {
        public const string ANIMATION_GROUP = "Animation settings";
        public const string EVENTS_GROUP = "Events";

        [SerializeField] [Group(ANIMATION_GROUP)]
        private bool resetAnimationOnStartPlaying = true;

        [SerializeField] [Group(ANIMATION_GROUP)]
        private bool resetSpeedOnCompletePlaying = true;

        [SerializeField] [Group(ANIMATION_GROUP)]
        private bool autoPlayOnEnable = false;

        [SerializeField] [Group(EVENTS_GROUP)]
        protected UnityEvent onStarted = new();

        [SerializeField] [Group(EVENTS_GROUP)]
        protected UnityEvent onCompleted = new();

        public bool IsPlaying { get; private set; }
        public bool IsSpeedMultiplied => !Mathf.Approximately(SpeedMultiplier, 1f);

        public abstract float SpeedMultiplier { get; }

        private void OnEnable()
        {
            if (autoPlayOnEnable)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            if (IsPlaying)
            {
                StopAnimation();
            }
        }

        public void Play()
        {
            PlayAsync().Forget();
        }

        public async UniTask PlayAsync()
        {
            try
            {
                if (resetAnimationOnStartPlaying)
                {
                    ResetAnimation();
                }

                IsPlaying = true;

                // We need to wait for one frame because of strange issue with DOTween and UniTask
                await UniTask.Yield();
                destroyCancellationToken.ThrowIfCancellationRequested();

                onStarted.Invoke();
                await InternalPlay();
                destroyCancellationToken.ThrowIfCancellationRequested();

                if (resetSpeedOnCompletePlaying)
                {
                    ResetSpeedMultiplier();
                }

                onCompleted.Invoke();
                IsPlaying = false;
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        public void ResetSpeedMultiplier()
        {
            ApplySpeedMultiplier(1f);
        }

        public abstract void ApplySpeedMultiplier(float value);

        public abstract void ResetAnimation();
        public abstract void CompleteAnimation();
        public abstract void StopAnimation();

        protected abstract UniTask InternalPlay();
    }
}