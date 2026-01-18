using System.Analytic.UserProperties;
using System.Collections.Generic;

namespace System.Analytic.Core.Cache
{
    public class UserPropertyCache
    {
        private readonly Queue<IUserProperty> propertiesCache = new();

        public bool Next(out IUserProperty userProperty)
        {
            userProperty = null;

            if (propertiesCache.Count <= 0)
            {
                return false;
            }

            userProperty = propertiesCache.Dequeue();
            return true;
        }

        public void Append(IUserProperty userProperty)
        {
            propertiesCache.Enqueue(userProperty);
        }
    }
}