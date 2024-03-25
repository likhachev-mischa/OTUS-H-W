using System.Collections.Generic;
using UnityEngine;
// ReSharper disable UnusedMember.Global

namespace Atomic.Objects
{
    public class AtomicEntityProxy : MonoBehaviour, IAtomicObject
    {
        public AtomicEntity Source
        {
            get => this.source;
            set => this.source = value;
        }

        [SerializeField]
        private AtomicEntity source;

        public T Get<T>(string key) where T : class
        {
            return this.source.Get<T>(key);
        }

        public bool TryGet<T>(string key, out T result) where T : class
        {
            return this.source.TryGet(key, out result);
        }

        public object Get(string key)
        {
            return this.source.Get(key);
        }

        public bool TryGet(string key, out object result)
        {
            return this.source.TryGet(key, out result);
        }

        public KeyValuePair<string, object>[] Properties()
        {
            return this.source.Properties();
        }

        public bool Is(string type)
        {
            return this.source.Is(type);
        }

        public bool Is(params string[] types)
        {
            return this.source.Is(types);
        }

        public string[] Types()
        {
            return this.source.Types();
        }
    }
}