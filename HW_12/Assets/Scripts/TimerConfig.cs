using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Configs/TimerConfig", fileName = "TimerConfig")]
    public sealed class TimerConfig : ScriptableObject
    {
        public float Time;
    }
}