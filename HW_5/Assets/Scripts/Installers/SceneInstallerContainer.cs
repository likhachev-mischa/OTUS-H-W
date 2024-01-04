using DI;
using UnityEngine;

namespace Installers
{
    public class SceneInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] private GameSaversInstaller gameSaversInstaller = new();
        [SerializeField] [GameInstaller] private UnitRepository unitRepository = new();
        [SerializeField] [GameInstaller] private SaveLoadInstaller saveLoadInstaller = new();
    }
}