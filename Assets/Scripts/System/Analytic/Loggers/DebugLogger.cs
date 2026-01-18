using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace System.Analytic.Loggers
{
    public class DebugLogger
    {
        private readonly bool enableDebug;

        public DebugLogger(bool enableDebug)
        {
            this.enableDebug = enableDebug;
        }

        [Conditional("DEBUG")]
        protected void DebugLog(string message)
        {
            if (!enableDebug)
            {
                return;
            }

            Debug.Log(message);
        }
    }
}