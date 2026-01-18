using System.Analytic.Loggers;
using System.Analytic.Modules;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Analytics.Firebase
{
    public class FirebaseAnalyticModule : AnalyticModuleBase
    {
        //private readonly IPlayerProfile playerProfile;
        private readonly EventDebugLogger logger;

        public FirebaseAnalyticModule(
            FirebaseModulesSettings settings
            /*IPlayerProfile playerProfile*/)
        {
            logger = new EventDebugLogger(settings.EnableEventDebug);
            //this.playerProfile = playerProfile;
        }

        protected override async UniTask InitializeInternal(CancellationToken token)
        {
            // Firebase specific initialization
        }

        public override void SendEvent(string eventName, IReadOnlyDictionary<string, string> parameters)
        {
            // FirebaseAnalytics.LogEvent(eventName, ConvertToFirebaseParameters(parameters));
        }

        public override void LogEvent(string eventName, IReadOnlyDictionary<string, string> parameters)
        {
            logger.DebugEvent("[Firebase]", eventName, parameters);
        }

        // From General string to string Dictionary to Parameter Array conversion:
        /*private static Parameter[] ConvertToFirebaseParameters(IReadOnlyDictionary<string, string> eventParameters)
        {
            var firebaseParameters = new Parameter[eventParameters.Count];

            var i = 0;
            foreach (var parameter in eventParameters)
            {
                firebaseParameters[i++] = new Parameter(parameter.Key, parameter.Value);
            }

            return firebaseParameters;
        }*/
    }
}