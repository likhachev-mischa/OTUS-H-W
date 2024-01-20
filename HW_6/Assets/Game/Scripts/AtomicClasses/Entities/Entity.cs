using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Entity : MonoBehaviour, IEntity
    {
        private readonly List<object> components = new();
        
        public void Add<T>(T component)
        {
            components.Add(component);
        }

        public T Get<T>()
        {
            for (var i = 0; i < components.Count; i++)
            {
                if (components[i] is T value)
                {
                    return value;
                }
            }

            throw new Exception($"Component {typeof(T)} not found!");
        }

        public bool TryGet<T>(out T component)
        {
            for (var i = 0; i < components.Count; i++)
            {
                if (components[i] is T value)
                {
                    component = value;
                    return true;
                }
            }

            component = default;
            return false;
        }
    }
}