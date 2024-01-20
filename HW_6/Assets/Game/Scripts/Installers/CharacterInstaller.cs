using System;
using DI;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public sealed class CharacterInstaller : GameInstaller
    {
        [Listener] [Service(typeof(CharacterEntity))] [SerializeField]
        private CharacterEntity characterEntity;

        [Service(typeof(Character))] [Listener] [SerializeField]
        private Character character;

        [SerializeField] [Listener] private CharacterVisuals characterVisuals;
    }
}