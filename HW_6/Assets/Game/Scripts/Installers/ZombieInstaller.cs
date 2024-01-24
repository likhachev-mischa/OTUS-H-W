using System;
using DI;
using UnityEngine;

namespace Game.Installers
{
    [Serializable]
    public sealed class ZombieInstaller : GameInstaller
    {
        [Listener] [SerializeField] private ZombieSpawner zombieSpawner = new();

        [Listener] [Service(typeof(ZombieSystem))] [SerializeField]
        private ZombieSystem zombieSystem = new();

        [Listener] private ZombiesController zombiesController = new();

        [Service(typeof(ZombieSpawns))] [SerializeField]
        private ZombieSpawns zombieSpawns;
    }
}