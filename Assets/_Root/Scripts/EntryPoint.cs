using Profile;
using UnityEngine;
using Tool.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Services;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    //[SerializeField] private UnityAdsService _adsService;
    //[SerializeField] private AnalyticsManager _analyticsManager;


    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        //ServicesSingltone.AdsService.InterstitialPlayer.Play();
       //_analyticsManager.SendMainMenuOpendEvent();

        //if (_adsService.IsInitialized) OnAdsInitialized();
        //else _adsService.Initialized.AddListener(OnAdsInitialized);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
    }

    //private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
