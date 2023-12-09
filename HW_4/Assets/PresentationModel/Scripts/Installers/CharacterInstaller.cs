using System;
using UnityEngine;

namespace MVVM
{
    [Serializable]
    public sealed class CharacterInstaller : GameInstaller
    {
        [SerializeField] [Service(typeof(Character))]
        private Character character;

        [Service(typeof(CharacterStatsProvider))] [SerializeField]
        private CharacterStatsProvider characterStatsProvider = new();

        [SerializeField] [Service(typeof(User))]
        private User user;
    }
}