using UnityEngine;

namespace MVVM
{
    public sealed class Character : MonoBehaviour
    {
        public CharacterInfo CharacterInfo
        {
            get => characterInfo;
        }

        public CharacterLevel CharacterLevel
        {
            get => characterLevel;
        }

        [SerializeField] private CharacterInfo characterInfo = new();
        [SerializeField] private CharacterLevel characterLevel = new();
        
    }
}