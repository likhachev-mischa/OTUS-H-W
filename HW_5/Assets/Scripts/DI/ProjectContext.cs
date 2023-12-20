using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class ProjectContext : Context
    {
        [SerializeField] private GameInstallerContainer projectInstallerContainer;

        private SceneContext sceneContext;
        private GameInstaller[] projectInstallers;
        
        public void RegisterProject()
        {
            serviceLocator = new ServiceLocator();
            projectInstallers = projectInstallerContainer.ProvideInstallers().ToArray();

            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        public void StartProject()
        {
            foreach (GameInstaller installer in projectInstallers)
            {
                ExtractInjectors(installer);
            }
            InjectGameObjectsOnScene();
        }
        
        public void RegisterScene()
        {
            sceneContext = FindObjectOfType<SceneContext>();
            sceneContext.RegisterServices(serviceLocator,gameManager);
        }

        public void StartScene()
        {
            sceneContext.Inject();
        }

        public void UnloadScene()
        {
            sceneContext.Unload();
        }

        
    }
}