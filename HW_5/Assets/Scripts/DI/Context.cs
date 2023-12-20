using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public abstract class Context : MonoBehaviour
    {
        [SerializeField] protected GameManager gameManager;
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

                IEnumerable<Type> serviceCollection = serviceProvider.ProvideServiceCollection();
                serviceLocator.BindService(typeof(IEnumerable<Type>),serviceCollection);
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