using System;
using DI;
using Game.UI;
using LoadSystem;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public sealed class MenuInstaller : GameInstaller
    {
        [Service(typeof(ApplicationLoader))] [SerializeField]
        private ApplicationLoader applicationLoader;

        [SerializeField] private MainMenuAdapter mainMenuAdapter;
    }
}