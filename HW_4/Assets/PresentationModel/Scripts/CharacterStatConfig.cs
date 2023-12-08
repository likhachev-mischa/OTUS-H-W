using UnityEngine;

namespace MVVM
{
    [CreateAssetMenu]
    public sealed class CharacterStatConfig : ScriptableObject
    {
        public string Name;
        public int Value;
    }
}