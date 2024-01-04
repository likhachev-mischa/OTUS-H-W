using DI;

namespace Installers
{
    public class ProjectInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] private RepositoryInstaller repositoryInstaller = new();
    }
}