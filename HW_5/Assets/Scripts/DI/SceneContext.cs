using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DI
{
    public sealed class SceneContext : Context
    {
        [SerializeField] private GameManager gameManager;

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