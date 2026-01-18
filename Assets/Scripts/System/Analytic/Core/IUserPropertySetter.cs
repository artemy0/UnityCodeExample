using System.Analytic.UserProperties;

namespace System.Analytic.Core
{
    public interface IUserPropertySetter
    {
        void SetUserProperty(IUserProperty userProperty);
    }
}