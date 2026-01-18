using System;
using System.Collections.Generic;
using R3;
using UI.General.Binder;
using UnityEngine;

namespace UI.General.Elements.Base
{
    [RequireComponent(typeof(UIBinderController))]
    public abstract partial class BaseViewModel : MonoBehaviour, IViewModel, ISerializationCallbackReceiver
    {
        private readonly Dictionary<string, object> observableProperties = new();
        private readonly Dictionary<string, Action> bindableMethods = new();

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            observableProperties.Clear();
            bindableMethods.Clear();
            GatherObservableProperties();
            GatherBindableMethods();
        }

        public bool TryGetObservableProperty<T>(string propertyName, out Observable<T> property)
        {
            property = null;

            if (observableProperties.Count == 0)
            {
                GatherObservableProperties();
            }

            if (observableProperties.TryGetValue(propertyName, out var propertyObject))
            {
                if (propertyObject is Observable<T> observable)
                {
                    property = observable;
                    return true;
                }
            }

            Debug.LogError($"Property {propertyName} not found", this);
            return false;
        }

        public bool TryGetMethodAction(string methodName, out Action method)
        {
            if (bindableMethods.Count == 0)
            {
                GatherBindableMethods();
            }

            if (bindableMethods.TryGetValue(methodName, out method))
            {
                return true;
            }

            Debug.LogError($"Method {methodName} not found", this);
            return false;
        }

#if UNITY_EDITOR
        public IEnumerable<string> GetObservablePropertyNames<T>()
        {
            if (observableProperties.Count == 0)
            {
                GatherObservableProperties();
            }

            foreach (var (propertyName, observableProperty) in observableProperties)
            {
                if (observableProperty is Observable<T>)
                {
                    yield return propertyName;
                }
            }
        }

        public IEnumerable<string> GetBindableMethodNames()
        {
            if (bindableMethods.Count == 0)
            {
                GatherBindableMethods();
            }

            return bindableMethods.Keys;
        }
#endif

#region ToAutoGenerate
        /// <summary>
        /// Auto-generated override.
        /// Do NOT implement manually.
        /// Used by the source generator to register observable properties.
        /// </summary>
        protected abstract void GatherObservableProperties();

        /// <summary>
        /// Auto-generated override.
        /// Do NOT implement manually.
        /// Used by the source generator to register bindable methods.
        /// </summary>
        protected abstract void GatherBindableMethods();
#endregion

        protected void AddObservableProperty(string propertyName, object observable)
        {
            observableProperties.Add(propertyName, observable);
        }

        protected void AddBindableMethod(string methodName, Action method)
        {
            bindableMethods.Add(methodName, method);
        }
    }
}