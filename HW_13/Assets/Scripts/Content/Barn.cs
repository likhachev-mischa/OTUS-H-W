using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Sample
{
    [Is(ObjectType.ResourceStorage)]
    public sealed class Barn : AtomicObject
    {
        ///Interface
        [Get(ObjectAPI.ResourceStorage)]
        public ResourceStorage Storage => this.storage;

        [Get(ObjectAPI.Transform)]
        public Transform Transform => this.transform;
        
        ///Core
        [SerializeField]
        private ResourceStorage storage;

        private void Awake()
        {
            this.Compose();
        }
    }
}