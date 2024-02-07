using System;
using System.Collections.Generic;

namespace DI
{
    public sealed class ObjectResolver : IObjectResolver
    {
        private readonly ServiceLocator serviceLocator;
        private readonly List<Type> services;

        public ObjectResolver(ServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            services = new List<Type>();
        }

        public T CreateInstance<T>() where T : new()
        {
            T instance = new();
            DependencyInjector.Inject(instance, serviceLocator);
            services.Add(typeof(T));
            return instance;
        }

        public void Dispose()
        {
            for (var i = 0; i < services.Count; i++)
            {
                serviceLocator.RemoveService(services[i]);
            }
            services.Clear();
        }
    }
}