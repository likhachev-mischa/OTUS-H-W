using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class Timer
    {
        public event Action TimerFinished;

        public float RequiredTime { get; }
        [field: SerializeField] public float TimeLeft { get; private set; }

        private bool isRunning;

        public Timer(TimerConfig config)
        {
            RequiredTime = config.Time;
            TimeLeft = RequiredTime;
            isRunning = false;
        }

        public void StartTimer()
        {
            ResetTimer();
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!isRunning)
            {
                return;
            }

            TimeLeft -= deltaTime;
            if (TimeLeft <= 0f)
            {
                isRunning = false;
                TimerFinished?.Invoke();
            }
        }

        public void ResetTimer()
        {
            isRunning = true;
            TimeLeft = RequiredTime;
        }
    }
}