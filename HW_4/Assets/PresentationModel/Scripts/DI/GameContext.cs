using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace MVVM
{
    public sealed class GameContext : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private ServiceLocator serviceLocator;

        [SerializeField] private MonoBehaviour[] monoInstallers;

        [SerializeField] private GameInstallerContainer installerContainer;

        private GameInstaller[] gameInstallers;

        private void Awake()
        {
            gameInstallers = installerContainer.ProvideInstallers().ToArray();

            foreach (MonoBehaviour installer in monoInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }

            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractListeners(installer);

                ExtractServices(installer);
            }
        }

        private void ExtractServices(object installer)
        {
            if (installer is IServiceProvider serviceProvider)
            {
                IEnumerable<(Type, object)> services = serviceProvider.ProvideServices();
                foreach ((Type type, object service) in services)
                {
                    serviceLocator.BindService(type, service);
                }
            }
        }

        private void ExtractListeners(object installer)
        {
            if (installer is IGameListenerProvider listenerProvider)
            {
                gameManager.AddListeners(listenerProvider.ProvideListeners());
            }
        }

        private void Start()
        {
            foreach (MonoBehaviour installer in monoInstallers)
            {
                ExtractInjectors(installer);
            }

            foreach (GameInstaller installer in gameInstallers)
            {
                ExtractInjectors(installer);
            }

            InjectGameObjectsOnScene();
        }

        private void ExtractInjectors(object installer)
        {
            if (installer is IInjectProvider injectProvider)
            {
                injectProvider.Inject(serviceLocator);
            }
        }

        private void InjectGameObjectsOnScene()
        {
            GameObject[] gameObjects = gameObject.scene.GetRootGameObjects();

            foreach (GameObject go in gameObjects)
            {
                Inject(go.transform);
            }
        }

        private void Inject(Transform targetTransform)
        {
            MonoBehaviour[] targets = targetTransform.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour target in targets)
            {
                DependencyInjector.Inject(target, serviceLocator);
            }

            foreach (Transform child in targetTransform)
            {
                Inject(child);
            }
        }
    }
}