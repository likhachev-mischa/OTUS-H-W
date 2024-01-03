using System;
using DI;
using GameEngine;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class SaveLoadInstaller : GameInstaller
    {
        [Service(typeof(Context))] [SerializeField]
        private SceneContext sceneContext;

        [Service(typeof(ResourceService))] private ResourceService resourceService = new();

        [Service(typeof(UnitManager))] private UnitManager unitManager = new();
    }
}