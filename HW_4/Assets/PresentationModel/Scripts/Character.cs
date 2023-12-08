using UnityEngine;

namespace MVVM
{
    public sealed class Character : MonoBehaviour
    {
        public CharacterInfo CharacterInfo { get=>characterInfo; }
        
        [SerializeField] private CharacterInfo characterInfo = new();


    }
}