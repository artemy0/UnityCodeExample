using System;
using System.Collections.Generic;
using R3;

namespace UI.General.Elements.Base
{
    public interface IViewModel
    {
        bool TryGetObservableProperty<T>(string propertyName, out Observable<T> property);
        bool TryGetMethodAction(string methodName, out Action method);

#if UNITY_EDITOR
        IEnumerable<string> GetObservablePropertyNames<T>();
        IEnumerable<string> GetBindableMethodNames();
#endif
    }
}