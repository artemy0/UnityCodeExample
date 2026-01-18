using System.Collections.Generic;
using TriInspector;
using UI.General.Elements.Base;
using UnityEngine;

namespace UI.General.Binder
{
    [ExecuteAlways]
    [RequireComponent(typeof(IViewModel))]
    public partial class UIBinderController : MonoBehaviour
    {
        [SerializeField]
#if UNITY_EDITOR
        [ReadOnly]
#endif
        private List<BaseBinderComponent> registeredBinders = new();

        private IViewModel viewModel;
        private IViewModel ViewModel => viewModel ??= GetComponent<IViewModel>();

        private void Awake()
        {
            if (Application.isPlaying)
            {
                viewModel = GetComponent<IViewModel>();
                BindAll();
            }
        }

        private void OnDestroy()
        {
            UnbindAll();
        }

        private void Reset()
        {
            RegisterAllBinders();
        }

        public void RegisterBinder(BaseBinderComponent baseBinder)
        {
            if (registeredBinders.Contains(baseBinder))
            {
                return;
            }

            registeredBinders.Add(baseBinder);
        }

        public void UnregisterBinder(BaseBinderComponent binder)
        {
            registeredBinders.Remove(binder);
        }

        private void BindAll()
        {
            foreach (var basicBinder in registeredBinders)
            {
                if (!basicBinder)
                {
                    continue;
                }

                basicBinder.Bind(ViewModel);
            }
        }

        private void UnbindAll()
        {
            foreach (var basicBinder in registeredBinders)
            {
                if (!basicBinder)
                {
                    continue;
                }

                basicBinder.Unbind(ViewModel);
            }
        }

        private void RegisterAllBinders()
        {
            var binders = GetComponentsInChildren<BaseBinderComponent>();
            foreach (var binder in binders)
            {
                if (binder.MyViewModel == ViewModel)
                {
                    RegisterBinder(binder);
                }
            }
        }
    }
}