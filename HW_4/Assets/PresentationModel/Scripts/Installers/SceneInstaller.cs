using UnityEngine;

namespace MVVM
{
    public sealed class SceneInstaller : GameInstallerContainer
    {
        [GameInstaller] [SerializeField] private CharacterInstaller characterInstaller = new();
    }
}