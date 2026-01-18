using TriInspector;
using UI.General.Elements.Base;
using UnityEngine;

namespace UI.General.Binder
{
    [ExecuteAlways]
    public abstract class BaseBinderComponent : MonoBehaviour
    {
        [SerializeField]
        private BaseViewModel viewModel;

        public IViewModel MyViewModel => viewModel;

        protected void OnDestroy()
        {
            if (!viewModel)
            {
                return;
            }

            var binderController = viewModel.GetComponent<UIBinderController>();
            binderController.UnregisterBinder(this);
        }

        private void Reset()
        {
            if (!TryGetComponent(out viewModel))
            {
                viewModel = GetComponentInParent<BaseViewModel>();
            }
        }

#if UNITY_EDITOR
        public abstract TriValidationResult ValidateBindings(IViewModel viewModel);
#endif
        
        public abstract void Bind(IViewModel viewModel);

        public abstract void Unbind(IViewModel viewModel);
    }
}