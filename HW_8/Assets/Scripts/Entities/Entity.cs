using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        private readonly Dictionary<Type, object> dictionary = new();

        public void Add<T>(T component)
        {
            Type type = typeof(T);
            if (dictionary.ContainsKey(type))
            {
                dictionary[type] = component;
                return;
            }

            dictionary.Add(type, component);
        }

        public bool TryGet<T>(out T component)
        {
            Type type = typeof(T);
            if (!dictionary.ContainsKey(type))
            {
                component = default;
                return false;
            }

            component = (T)dictionary[type];
            return true;
        }

        public T Get<T>()
        {
            Type type = typeof(T);
            return (T)dictionary[type];
        }
    }
}