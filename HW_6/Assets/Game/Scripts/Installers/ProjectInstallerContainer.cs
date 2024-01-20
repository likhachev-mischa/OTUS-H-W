using DI;
using LoadSystem;
using UnityEngine;

namespace Game.Installers
{
    public sealed class ProjectInstallerContainer : GameInstallerContainer
    {
        [SerializeField] [GameInstaller] private MenuInstaller menuInstaller = new();
    }
}