using DI;
using UnityEngine;

namespace Game.Installers
{
    public sealed class SceneInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] [SerializeField] private ControllersInstaller controllersInstaller = new();
        [GameInstaller] [SerializeField] private CharacterInstaller characterInstaller = new();
    }
}