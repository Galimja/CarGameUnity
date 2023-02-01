using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Analytics
{
    internal interface IAnalyticsManager
    {
        void SendMainMenuOpendEvent();
        
        void SendGameStartEvent();

        void SendTransaction(string productId, decimal amount, string currency);
    }


    internal class AnalyticsManager : MonoBehaviour, IAnalyticsManager
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void SendTransaction(string productId, decimal amount, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
            {
                _services[i].SendTransaction(productId, amount, currency);
            }

            Log($"Sent Transaction {productId}");
        }

        public void SendMainMenuOpendEvent() =>
            SendEvent("MainMenuOpend");

        public void SendGameStartEvent() =>
            SendEvent("Game started!");

        public void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services)
            {
                service.SendEvent(eventName);
            }
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services)
            {
                service.SendEvent(eventName, eventData);
            }
        }

        private void Log(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}