#if SW_STAGE_STAGE1_OR_ABOVE
using System;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Setup;
using UnityEngine;

namespace SupersonicWisdomSDK
{
    public delegate void OnGameAnalyticsInit ();

    internal static class SwStage1GameAnalyticsConstants
    {
        #region --- Constants ---

        public const string GameAnalyticsInitInternalEventName = "GameAnalyticsInit";
        public const string GameAnalyticsInitSkippedInternalEventName = "GameAnalyticsInitSkipped";

        #endregion
    }

    internal class SwStage1GameAnalyticsAdapter : ISwGameProgressionListener, ISwAdapter
    {
        #region --- Events ---

        internal event OnGameAnalyticsInit OnGameAnalyticsInitEvent;

        #endregion


        #region --- Properties ---

        public bool DidInitializeGameAnalytics { get; private set; }
        public bool IsGameAnalyticsInitialized { get; private set; }

        #endregion


        #region --- Members ---

        private string _sdkStatus;
        
        #endregion


        #region --- Public Methods ---

        public void Init ()
        {
            if (DidInitializeGameAnalytics)
            {
                return;
            }

            DidInitializeGameAnalytics = true;

            if (CanInit() || Application.isEditor)
            {
                GameAnalytics.Initialize();
                IsGameAnalyticsInitialized = true;
                AfterInit(true);
            }
            else
            {
                AfterInit(false);
            }
        }

        public void OnLevelCompleted(ESwLevelType levelType, long level, string customString, long attempts, long revives)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, GetLevelName(level), levelType.ToString(), customString);
        }

        public void OnTimeBasedGameStarted () { }
        
        public void OnLevelFailed(ESwLevelType levelType, long level, string customString, long attempts, long revives)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, GetLevelName(level), levelType.ToString(), customString);
        }

        public void OnLevelRevived(ESwLevelType levelType, long level, string customString, long attempts, long revives) { }

        public void OnLevelSkipped(ESwLevelType levelType, long level, string customString, long attempts, long revives)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, GetLevelName(level),  levelType.ToString(), customString);
        }

        public void OnLevelStarted(ESwLevelType levelType, long level, string customString, long attempts, long revives)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, GetLevelName(level),  levelType.ToString(), customString);
        }

        public void OnMetaStarted(string customString)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, ESwGameplayType.Meta.ToString(), customString);
        }
        
        public void OnMetaEnded(string customString)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, ESwGameplayType.Meta.ToString(), customString);
        }

        public void SendDesignEvent(string eventName, float eventType)
        {
            if (!IsGameAnalyticsInitialized) return;
            GameAnalytics.NewDesignEvent(eventName, eventType);
        }
        
        public SwAdapterData GetAdapterStatusAndVersion()
        {
            var adapterData = new SwAdapterData
            {
                adapterName = nameof(GameAnalytics),
                adapterStatus = _sdkStatus,
                adapterVersion = Settings.VERSION
            };

            return adapterData;
        }

        #endregion


        #region --- Private Methods ---

        protected virtual void AfterInit(bool didInit)
        {
            _sdkStatus = didInit.ToString();
            
            if (didInit)
            {
                SwInfra.Logger.Log("GameAnalytics | init");
                SwInternalEvent.Invoke(SwStage1GameAnalyticsConstants.GameAnalyticsInitInternalEventName);

                try
                {
                    OnGameAnalyticsInitEvent?.Invoke();
                }
                catch (Exception e)
                {
                    SwInfra.Logger.LogError($"OnGameAnalyticsInitEvent Invoke Error: {e.Message}");
                    _sdkStatus = e.Message;
                }
            }
            else
            {
                SwInternalEvent.Invoke(SwStage1GameAnalyticsConstants.GameAnalyticsInitSkippedInternalEventName);
            }
        }

        protected virtual bool CanInit ()
        {
            return true;
        }

        private string GetLevelName(long level)
        {
            return "level_" + level.ToString("D3"); // e.g "level_007"
        }

        #endregion
    }
}
#endif