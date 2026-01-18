using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Analytic.Loggers
{
    public class EventDebugLogger : DebugLogger
    {
        private readonly StringBuilder debugMessage = new();

        public EventDebugLogger(bool enableDebug) : base(enableDebug)
        {
        }

        [Conditional("DEBUG")]
        public void DebugEvent(string title, string eventName, IReadOnlyDictionary<string, string> parameters)
        {
            debugMessage.Clear();

            debugMessage.Append($"[Analytics] {title} EVENT: {eventName}\n");
            AppendParameters(parameters);

            DebugLog(debugMessage.ToString());
        }

        private void AppendParameters(IReadOnlyDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            debugMessage.Append("PARAMETERS: ");

            var i = 0;
            foreach (var parameter in parameters)
            {
                debugMessage.Append($"{parameter.Key}: {parameter.Value}");
                if (++i < parameters.Count)
                {
                    debugMessage.Append(", ");
                }
            }
        }
    }
}