using System.Diagnostics;

namespace System.Analytic.Loggers
{
    public class UserPropertyDebugLogger : DebugLogger
    {
        public UserPropertyDebugLogger(bool enableDebug) : base(enableDebug)
        {
        }

        [Conditional("DEBUG")]
        public void DebugUserProperty(string title, string userPropertyName, string value)
        {
            DebugLog($"[Analytics] {title} USER PROPERTY: {userPropertyName} = {value}");
        }
    }
}