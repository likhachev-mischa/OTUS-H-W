using UnityEngine;

namespace MVVM
{
    [CreateAssetMenu(menuName = "Configs/Character Stat", fileName = "Character Stat")]
    public sealed class CharacterStatConfig : ScriptableObject
    {
        public string Name;
        public int Value;
    }
}