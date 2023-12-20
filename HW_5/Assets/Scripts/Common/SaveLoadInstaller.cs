using System;
using DI;
using SaveSystem;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class SaveLoadInstaller : GameInstaller
    {
        [Service(typeof(GameRepository))]
        private GameRepository gameRepository = new();

        [Service(typeof(Context))]
        [SerializeField] private SceneContext sceneContext;
    }
}