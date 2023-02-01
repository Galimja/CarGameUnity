using System;
using System.Collections.Generic;

namespace Tool.Analytics
{
    internal class Dev2DevAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName)
        {
            throw new NotImplementedException();
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            throw new NotImplementedException();
        }

        public void SendTransaction(string productId, decimal amount, string currency)
        {
            throw new NotImplementedException();
        }
    }
}
