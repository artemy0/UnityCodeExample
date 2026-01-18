using System.Collections.Generic;

namespace System.Analytic.Events
{
    public abstract class BaseAnalyticEvent : IAnalyticEvent
    {
        private readonly Dictionary<string, string> parameters = new();

        public string this[string key]
        {
            get => parameters.GetValueOrDefault(key);
            set => parameters[key] = value;
        }

        public abstract string Name { get; }
        public IReadOnlyDictionary<string, string> Parameters => parameters;

        public abstract void Release();

        public void Dispose()
        {
            parameters.Clear();
        }
    }
}