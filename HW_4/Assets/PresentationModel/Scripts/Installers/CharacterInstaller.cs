using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterInstaller
    {
        [SerializeField] [Service(typeof(Character))]
        private Character character;

    }
}