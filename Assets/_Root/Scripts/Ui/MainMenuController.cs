using Profile;
using Tool;
using UnityEngine;
using UnityEngine.Analytics;
using Object = UnityEngine.Object;
using Services;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, ToSettings, PlayRewardedAds, BuyProduct);

            ServicesSingltone.AnalyticsManager.SendMainMenuOpendEvent();

            SubscribeAds();
            SubscribeIAP();
        }

        protected override void OnDispose()
        {
            UnSubscribeAds();
            UnSubscribeIAP();
        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void ToSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void PlayRewardedAds() =>
            ServicesSingltone.AdsService.RewardedPlayer.Play();

        private void BuyProduct(string productId) =>
            ServicesSingltone.IapService.Buy(productId);

        private void SubscribeAds()
        {
            ServicesSingltone.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServicesSingltone.AdsService.RewardedPlayer.Skipped += OnAdsCanceled;
            ServicesSingltone.AdsService.RewardedPlayer.Failed += OnAdsCanceled;
        }

        private void UnSubscribeAds()
        {
            ServicesSingltone.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServicesSingltone.AdsService.RewardedPlayer.Skipped -= OnAdsCanceled;
            ServicesSingltone.AdsService.RewardedPlayer.Failed -= OnAdsCanceled;
        }

        private void SubscribeIAP()
        {
            ServicesSingltone.IapService.PurchaseSucceed.AddListener(OnAIPSucceed);
            ServicesSingltone.IapService.PurchaseFailed.AddListener(OnAIPFailed);
        }

        private void UnSubscribeIAP()
        {
            ServicesSingltone.IapService.PurchaseSucceed.RemoveListener(OnAIPSucceed);
            ServicesSingltone.IapService.PurchaseFailed.RemoveListener(OnAIPFailed);
        }

        private void OnAIPSucceed() => Debug.Log("Purchase succeed");

        private void OnAIPFailed() => Debug.Log("Purchase failed");

        private void OnAdsFinished() => Debug.Log("Ads viewed, good job!");

        private void OnAdsCanceled() => Debug.Log("Some problems with ads");
    }
}
