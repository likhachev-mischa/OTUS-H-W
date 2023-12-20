using DI;

namespace Common
{
    public class ProjectInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] private SaveLoadInstaller saveLoadInstaller = new();
    }
}