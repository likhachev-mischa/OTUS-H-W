using DI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class ProjectInstallerContainer : GameInstallerContainer
    {
        [GameInstaller] private RepositoryInstaller repositoryInstaller = new();
    }
}