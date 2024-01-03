using DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class SceneInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] private GameSaversInstaller gameSaversInstaller = new();
        [SerializeField] [GameInstaller] private SceneRepository sceneRepository = new();
        [GameInstaller] [SerializeField] private SaveLoadInstaller saveLoadInstaller = new();
    }
}