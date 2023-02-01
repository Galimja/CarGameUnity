using UnityEngine;
using Services.Ads.UnityAds;
using Services.Ads;
using Services.IAP;
using Tool.Analytics;

namespace Services
{
    internal class ServicesSingltone : MonoBehaviour
    {
        [SerializeField] private UnityAdsService _unityAdsService;
        [SerializeField] private IAPService _iapService;
        [SerializeField] private AnalyticsManager _analyticsManager;

        private static ServicesSingltone _instance;

        private static ServicesSingltone Instance 
        { 
            get => _instance ?? FindObjectOfType<ServicesSingltone>();          
        }

        internal static IAdsService AdsService => Instance._unityAdsService;

        internal static IIAPService IapService => Instance._iapService;

        internal static IAnalyticsManager AnalyticsManager => Instance._analyticsManager;

        private void Awake()
        {
            _instance ??= this;
        }
    }
}