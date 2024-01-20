using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class SceneContext : Context
    {
        [SerializeField] private GameInstallerContainer sceneInstallerContainer;

        private GameInstaller[] gameInstallers;

        public void RegisterServices(ServiceLocator serviceLocator, GameManager gameManager)
        {
            this.serviceLocator = serviceLocator;
            this.gameManager = gameManager;

            gameInstallers = sceneInstallerContainer.ProvideInstallers().ToArray();

            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        public void Inject()
        {
            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractInjectors(installer);
            }

            InjectGameObjectsOnScene();
        }

        public void Unload()
        {
            foreach (GameInstaller installer in gameInstallers)
            {
                if (installer is IGameListenerProvider listenerProvider)
                {
                    gameManager.RemoveListeners(listenerProvider.ProvideListeners());
                }

                if (installer is IServiceProvider serviceProvider)
                {
                    IEnumerable<(Type, object)> services = serviceProvider.ProvideServices();
                    foreach ((Type type, object service) temp in services)
                    {
                        serviceLocator.RemoveService(temp.type);
                    }
                }
            }
        }

    }
}