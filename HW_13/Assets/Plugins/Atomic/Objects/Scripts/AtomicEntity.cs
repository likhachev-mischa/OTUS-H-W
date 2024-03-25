using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Atomic Entity")]
    public class AtomicEntity : MonoBehaviour, IAtomicObject
    {
        [Title("Data"), PropertySpace]
        
        //Properties:
        [ShowInInspector, HideInEditorMode, PropertyOrder(100)]
        private protected ISet<string> types = new HashSet<string>();

        [ShowInInspector, HideInEditorMode, PropertyOrder(100)]
        private protected IDictionary<string, object> properties = new Dictionary<string, object>();
        
        public bool Is(string type)
        {
            return this.types.Contains(type);
        }

        public bool Is(params string[] types)
        {
            for (int i = 0, count = types.Length; i < count; i++)
            {
                string type = types[i];
              
                if (!this.types.Contains(type))
                {
                    return false;
                }
            }

            return true;
        }

        public T Get<T>(string key) where T : class
        {
            if (this.properties.TryGetValue(key, out var value))
            {
                return value as T;
            }

            return default;
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            if (this.properties.TryGetValue(key, out var value))
            {
                result = value as T;
                return true;
            }

            result = default;
            return false;
        }

        public object Get(string key)
        {
            if (this.properties.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public bool TryGet(string key, out object result)
        {
            return this.properties.TryGetValue(key, out result);
        }

        public string[] Types()
        {
            return this.types.ToArray();
        }
        
        public KeyValuePair<string, object>[] Properties()
        {
            return this.properties.ToArray();
        }

        public bool AddProperty(string key, object value)
        {
            return this.properties.TryAdd(key, value);
        }

        public void SetProperty(string key, object value)
        {
            this.properties[key] = value;
        }

        public bool RemoveProperty(string key)
        {
            return this.properties.Remove(key);
        }

        public bool RemoveProperty(string key, out object value)
        {
            return this.properties.Remove(key, out value);
        }

        public void OverrideProperty(string key, object value, out object prevValue)
        {
            this.properties.TryGetValue(key, out prevValue);
            this.properties[key] = value;
        }

        public bool AddType(string type)
        {
            return this.types.Add(type);
        }

        public void AddTypes(IEnumerable<string> types)
        {
            this.types.UnionWith(types);
        }

        public bool RemoveType(string type)
        {
            return this.types.Remove(type);
        }

        public void RemoveTypes(IEnumerable<string> types)
        {
            foreach (var type in types)
            {
                this.types.Remove(type);
            }
        }

        public ISet<string> TypesUnsafe()
        {
            return this.types;
        }

        public IDictionary<string, object> PropertiesUnsafe()
        {
            return this.properties;
        }

        public int TypesNonAlloc(string[] results)
        {
            int i = 0;
            
            foreach (var type in this.types)
            {
                results[i++] = type;
            }

            return i;
        }
        
        public int PropertiesNonAlloc(KeyValuePair<string, object>[] results)
        {
            int i = 0;
            
            foreach (var property in this.properties)
            {
                results[i++] = property;
            }
            
            return i;
        }
    }
}