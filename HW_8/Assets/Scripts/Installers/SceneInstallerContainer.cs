using DI;
using UnityEngine;

namespace Installers
{
    public sealed class SceneInstallerContainer : GameInstallerContainer
    {
        [SerializeField] [GameInstaller] private PipelineInstaller pipelineInstaller = new();

        [SerializeField] [GameInstaller] private ServicesInstaller servicesInstaller = new();

        [GameInstaller] private HandlersInstaller handlersInstaller = new();
    }
}