using DI;
using UnityEngine;

namespace Game.Installers
{
    public sealed class SceneInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] [SerializeField] private ControllersInstaller controllersInstaller = new();
        [GameInstaller] [SerializeField] private CharacterInstaller characterInstaller = new();
        [GameInstaller] [SerializeField] private BulletInstaller bulletInstaller = new();
        [GameInstaller] [SerializeField] private ZombieInstaller zombieInstaller = new();
        [GameInstaller] [SerializeField] private UIInstaller uiInstaller = new();
    }
}