using System;

namespace Game
{
    public interface IRealtimeTimer
    {
        event Action<string> TimerStarted;
        void SynchronizeTime(float time);
        void StartTimer();
        void ResumeTimer();
        string SaveKey { get; }
        Timer Timer { get; }
    }
}