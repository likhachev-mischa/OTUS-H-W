using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField] private GameInstallerContainer installerContainer;

        private IEnumerable<GameInstaller> installers;
        public void ResolveDependencies()
        {
            installers = installerContainer.ProvideInstallers();

            InitializeServiceLocator();
            InjectDependencies();
        }

        private void InitializeServiceLocator()
        {
            foreach (GameInstaller installer in installers)
            {
                ExtractServices(installer);
            }
        }
        
        private void InjectDependencies()
        {
            foreach (GameInstaller installer in installers)
            {
                ExtractInjectors(installer);
            }
        }
    }
}