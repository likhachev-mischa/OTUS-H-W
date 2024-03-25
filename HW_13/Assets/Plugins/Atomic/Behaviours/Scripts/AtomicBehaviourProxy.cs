using System.Collections.Generic;
using Atomic.Objects;
using UnityEngine;

namespace Atomic.Behaviours
{
    public class AtomicBehaviourProxy : MonoBehaviour, IAtomicObject, IAtomicBehaviour
    {
        public AtomicBehaviour Source
        {
            get => this.source;
            set => this.source = value;
        }

        [SerializeField]
        private AtomicBehaviour source;

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

        public void AddLogic(ILogic target)
        {
            this.source.AddLogic(target);
        }

        public void RemoveLogic(ILogic target)
        {
            this.source.RemoveLogic(target);
        }

        public ILogic[] LogicElements()
        {
            return this.source.LogicElements();
        }
    }
}