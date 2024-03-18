using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class Chest : IRealtimeTimer, IDisposable
    {
        public event Action<string> TimerStarted;
        public event Action<string, int> RewardClaimed;
        private int currencyAmount;
        private string currencyID;

        [field: SerializeField, ReadOnly] public string SaveKey { get; private set; }
        [field: SerializeField] public Timer Timer { get; private set; }
        [field: SerializeField] public bool IsReady { get; private set; }

        public Chest(ChestConfig config)
        {
            this.currencyAmount = config.CurrencyAmount;
            this.currencyID = config.CurrencyID;
            SaveKey = config.ChestID;
            Timer = new Timer(config.TimerConfig);
            Timer.TimerFinished += OnTimerFinished;
        }

        public void StartTimer()
        {
            Timer.StartTimer();
            IsReady = false;
            TimerStarted?.Invoke(SaveKey);
        }

        public void ResumeTimer()
        {
            Timer.StartTimer();
        }

        public void SynchronizeTime(float time)
        {
            Timer.UpdateTimer(time);
        }

        [Button]
        public void ClaimReward()
        {
            if (!IsReady)
            {
                Debug.Log($"{SaveKey} Chest is not opened yet!");
                return;
            }

            RewardClaimed?.Invoke(currencyID, currencyAmount);
            StartTimer();
        }

        private void OnTimerFinished()
        {
            IsReady = true;
            Debug.Log($"{SaveKey} Chest is ready to be opened!");
        }

        public void Dispose()
        {
            Timer.TimerFinished -= OnTimerFinished;
        }
    }
}