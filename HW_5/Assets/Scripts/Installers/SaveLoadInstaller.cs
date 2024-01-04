using System;
using DI;
using GameEngine;
using UnityEngine;

namespace Installers
{
    [Serializable]
    public class SaveLoadInstaller : GameInstaller
    {
        [Service(typeof(ResourceService))] private ResourceService resourceService = new();

        [SerializeField] [Service(typeof(UnitManager))]
        private UnitManager unitManager = new();
    }
}