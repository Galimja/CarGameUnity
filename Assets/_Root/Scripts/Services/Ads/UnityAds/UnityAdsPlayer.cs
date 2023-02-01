using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skipped;
        public event Action BecomeReady;

        protected readonly string Id;


        protected UnityAdsPlayer(string id)
        {
            Id = id;

        }


        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void OnPlaying();
        protected abstract void Load();

        #region IUnityAdsShowListener interface
        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {

            Error($"Error: {message}");
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Started");
            Started?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Ready");
            BecomeReady?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showResult)
        {
            if (IsIdMy(placementId) == false)
                return;

            switch (showResult)
            {

                case UnityAdsShowCompletionState.COMPLETED:
                    Log("Finished");
                    Finished?.Invoke();
                    break;

                case UnityAdsShowCompletionState.UNKNOWN:
                    Error("Failed");
                    Failed?.Invoke();
                    break;

                case UnityAdsShowCompletionState.SKIPPED:
                    Log("Skipped");
                    Skipped?.Invoke();
                    break;
            }
        }
        #endregion

        #region IUnityAdsLoadListener
        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Loaded");
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Load failed");
        }
        #endregion

        #region IUnityAdsListener interface
        //void IUnityAdsListener.OnUnityAdsReady(string placementId)
        //{
        //    if (IsIdMy(placementId) == false)
        //        return;

        //    Log("Ready");
        //    BecomeReady?.Invoke();
        //}

        //void IUnityAdsListener.OnUnityAdsDidError(string message) =>
        //    Error($"Error: {message}");

        //void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
        //{
        //    if (IsIdMy(placementId) == false)
        //        return;

        //    Log("Started");
        //    Started?.Invoke();
        //}

        //void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        //{
        //    if (IsIdMy(placementId) == false)
        //        return;

        //    switch (showResult)
        //    {
        //        case ShowResult.Finished:
        //            Log("Finished");
        //            Finished?.Invoke();
        //            break;

        //        case ShowResult.Failed:
        //            Error("Failed");
        //            Failed?.Invoke();
        //            break;

        //        case ShowResult.Skipped:
        //            Log("Skipped");
        //            Skipped?.Invoke();
        //            break;
        //    }
        //}
        #endregion

        private bool IsIdMy(string id) => Id == id;

        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";

        
    }
}
