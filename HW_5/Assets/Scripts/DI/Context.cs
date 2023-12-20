using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public abstract class Context : MonoBehaviour
    {
        protected readonly ServiceLocator serviceLocator = new();

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
            }
        }

        protected void ExtractInjectors(object installer)
        {
            if (installer is IInjectProvider injectProvider)
            {
                injectProvider.Inject(serviceLocator);
            }
        }
    }
}