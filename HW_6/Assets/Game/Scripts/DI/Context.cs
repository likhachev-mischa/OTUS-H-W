using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public abstract class Context : MonoBehaviour
    {
        protected GameManager gameManager;
        protected ServiceLocator serviceLocator;

        public object GetService(Type type)
        {
            return serviceLocator.GetService(type);
        }

        public T GetService<T>() where T : class
        {
            return serviceLocator.GetService<T>();
        }

        public void BindService(Type type, object service)
        {
            serviceLocator.BindService(type, service);
        }

        protected void ExtractServices(object installer)
        {
            if (installer is IServiceProvider serviceProvider)
            {
                IEnumerable<(Type, object)> services = serviceProvider.ProvideServices();
                foreach ((Type type, object service) in services)
                {
                    serviceLocator.BindService(type, service);
                }

                Dictionary<Type, List<object>> serviceCollections = serviceProvider.ProvideServiceCollections();

                foreach ((Type type, List<object> list) in serviceCollections)
                {
                    if (!type.IsArray)
                    {
                        throw new Exception("Type of ServiceCollection must be an array!");
                    }

                    var array = Array.CreateInstance(type.GetElementType(), list.Count);
                    for (int i = 0; i < array.Length; ++i)
                    {
                        array.SetValue(list[i], i);
                    }

                    serviceLocator.BindService(type, array);
                }
            }
        }

        protected void ExtractInjectors(object installer)
        {
            if (installer is IInjectProvider injectProvider)
            {
                injectProvider.Inject(serviceLocator);
            }
        }

        protected void ExtractListeners(object installer)
        {
            if (installer is IGameListenerProvider listenerProvider)
            {
                gameManager.AddListeners(listenerProvider.ProvideListeners());
            }
        }

        protected void InjectGameObjectsOnScene()
        {
            GameObject[] gameObjects = gameObject.scene.GetRootGameObjects();

            foreach (GameObject go in gameObjects)
            {
                Inject(go.transform);
            }
        }

        protected void Initialize()
        {
            serviceLocator = new ServiceLocator();
            gameManager = gameObject.AddComponent<GameManager>();
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