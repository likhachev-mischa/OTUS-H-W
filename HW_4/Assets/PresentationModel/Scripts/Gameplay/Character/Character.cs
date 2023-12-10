using UnityEngine;

namespace MVVM
{
    public sealed class Character : MonoBehaviour
    {
        public CharacterInfo CharacterInfo
        {
            get { return characterInfo; }
        }

        public CharacterLevel CharacterLevel
        {
            get { return characterLevel; }
        }

        [SerializeField] private CharacterInfo characterInfo = new();
        [SerializeField] private CharacterLevel characterLevel = new();
    }
}