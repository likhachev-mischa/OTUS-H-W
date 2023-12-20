using System;
using System.Collections.Generic;

namespace DI
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> services = new();

        public object GetService(Type type)
        {
            return services[type];
        }

        public T GetService<T>() where T : class
        {
            return services[typeof(T)] as T;
        }

        public void BindService(Type type, object service)
        {
            services.Add(type, service);
        }

        public void RemoveService(Type type)
        {
            services.Remove(type);
        }
        
        public void RemoveService<T>()
        {
            services.Remove(typeof(T));
        }

    }
}