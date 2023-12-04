using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ServiceLocator : MonoBehaviour
    {
        private readonly Dictionary<Type, object> services = new();

        public object GetService(Type type)
        {
            object service = services[type];

            return service;
        }

        public void BindService(Type type, object service)
        {
            services.Add(type, service);
        }
    }
}