using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Game
{
    public sealed class TimerSynchronizer : ISaveLoader
    {
        private HashSet<IRealtimeTimer> _realtimeTimers = new();

        public void RegisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Add(realtimeTimer);
            realtimeTimer.TimerStarted += OnTimerStarted;
        }

        public void UnregisterTimer(IRealtimeTimer realtimeTimer)
        {
            _realtimeTimers.Remove(realtimeTimer);
            realtimeTimer.TimerStarted -= OnTimerStarted;
        }

        public void OnLoadGame()
        {
            foreach (var realtimeTimer in _realtimeTimers)
            {
                if (!PlayerPrefs.HasKey(realtimeTimer.SaveKey))
                {
                    realtimeTimer.StartTimer();
                    continue;
                }

                realtimeTimer.ResumeTimer();
                Load(realtimeTimer);
            }
        }

        public void OnSaveGame()
        {
            //serialization logic instead of PlayerPrefs
        }

        public void UpdateTimers(float deltaTime)
        {
            foreach (IRealtimeTimer realtimeTimer in _realtimeTimers)
            {
                realtimeTimer.SynchronizeTime(deltaTime);
            }
        }

        private void OnTimerStarted(string id)
        {
            var now = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(id, now);
        }

        private void Load(IRealtimeTimer timer)
        {
            var offlineTime = CalculateOfflineTime(timer.SaveKey);
            timer.SynchronizeTime(offlineTime);
        }

        private float CalculateOfflineTime(string saveKey)
        {
            var savedTime = PlayerPrefs.GetString(saveKey);
            DateTime time = DateTime.Parse(savedTime, CultureInfo.InvariantCulture);

            var now = DateTime.Now;
            TimeSpan timeSpan = now - time;
            return (float)timeSpan.TotalSeconds;
        }
    }
}