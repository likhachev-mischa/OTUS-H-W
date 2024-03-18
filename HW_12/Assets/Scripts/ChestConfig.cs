using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Configs/ChestConfig", fileName = "ChestConfig")]
    public sealed class ChestConfig : ScriptableObject
    {
        public int CurrencyAmount;
        public string CurrencyID;
        public string ChestID;
        public TimerConfig TimerConfig;
    }
}