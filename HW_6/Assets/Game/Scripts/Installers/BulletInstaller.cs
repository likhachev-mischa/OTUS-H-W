using System;
using DI;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public sealed class BulletInstaller : GameInstaller
    {
        [Service(typeof(BulletLauncher))] private BulletLauncher bulletLauncher = new();

        [Listener] [Service(typeof(BulletSystem))] [SerializeField]
        private BulletSystem bulletSystem = new();
    }
}