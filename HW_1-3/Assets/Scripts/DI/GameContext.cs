using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private ServiceLocator serviceLocator;

        [SerializeField] private MonoBehaviour[] installers;

        private void Awake()
        {
            foreach (MonoBehaviour installer in installers)
            {
                if (installer is IGameListenerProvider listenerProvider)
                {
                    gameManager.AddListeners(listenerProvider.ProvideListeners());
                }

                if (installer is IServiceProvider serviceProvider)
                {
                    IEnumerable<(Type, object)> services = serviceProvider.ProvideServices();
                    foreach ((Type type, object service) in services)
                    {
                        serviceLocator.BindService(type, service);
                    }
                }
            }
        }

        private void Start()
        {
            foreach (MonoBehaviour installer in installers)
            {
                if (installer is IInjectProvider injectProvider)
                {
                    injectProvider.Inject(serviceLocator);
                }
            }

            InjectGameObjectsOnScene();
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