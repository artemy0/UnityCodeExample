using System.Analytic.Loggers;
using System.Analytic.Modules;
using System.Analytic.UserProperties;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Analytics.Firebase
{
    public class FirebaseUserPropertyModule : IUserPropertyModule
    {
        // TODO Add PlayerProfile

        private const int USER_PROPERTY_NAME_LENGTH_LIMIT = 24;
        private const int USER_PROPERTY_VALUE_LENGTH_LIMIT = 36;

        private readonly UserPropertyDebugLogger debugLogger;
        /*private readonly IPlayerProfile playerProfile;*/

        public FirebaseUserPropertyModule(
            FirebaseModulesSettings settings
            /*IPlayerProfile playerProfile*/)
        {
            debugLogger = new UserPropertyDebugLogger(settings.EnableUserPropertyDebug);
            /*this.playerProfile = playerProfile;*/
        }

        public UniTask Initialize()
        {
            SetUserProperty("...", "...");
            return UniTask.CompletedTask;
        }

        public void SetUserProperty(IUserProperty userProperty)
        {
            SetUserProperty(userProperty.Name, userProperty.Value);
        }

        private void SetUserProperty(string userPropertyName, string userPropertyValue)
        {
            // TODO Add Firebase
            /*
            FirebaseAnalytics.SetUserProperty(
                userPropertyName.Length > USER_PROPERTY_NAME_LENGTH_LIMIT
                    ? userPropertyName[..USER_PROPERTY_NAME_LENGTH_LIMIT]
                    : userPropertyName,
                userPropertyValue.Length > USER_PROPERTY_VALUE_LENGTH_LIMIT
                    ? userPropertyValue[..USER_PROPERTY_VALUE_LENGTH_LIMIT]
                    : userPropertyValue);
            */

            debugLogger.DebugUserProperty("[Firebase]", userPropertyName, userPropertyValue);
        }
    }
}