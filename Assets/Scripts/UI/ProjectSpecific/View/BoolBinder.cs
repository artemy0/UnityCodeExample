using System;
using TriInspector;
using UI.General.Binder;
using UI.General.Elements.Base;

namespace UI.ProjectSpecific.View
{
    public class BoolBinder : BaseBinderComponent // Change to PropertyBinder<bool>
    {
        // Update bool through UnityEvent (for example) base on ReactiveProperty data

        private IDisposable subscription;
        
        public override void Bind(IViewModel viewModel)
        {
            subscription = null;
            
            // Subscribe Update to data
        }

        public override void Unbind(IViewModel viewModel)
        {
            subscription?.Dispose();
        }

#if UNITY_EDITOR
        public override TriValidationResult ValidateBindings(IViewModel viewModel)
        {
            // Use reflection for ReactiveProperty validation
            
            return TriValidationResult.Valid;
        }
#endif
    }
}