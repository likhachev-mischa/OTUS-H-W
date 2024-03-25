using Atomic.Elements;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sample
{
    [Is(ObjectType.Resource)]
    public sealed class Tree : AtomicObject
    {
        ///Interface 
        [Get(ObjectAPI.ResourceStorage)]
        public ResourceStorage Storage => this.storage;

        [Get(ObjectAPI.Transform)]
        public Transform Transform => this.transform;

        [Get(ObjectAPI.IsActive)]
        public IAtomicValue<bool> IsActive => this.isActive;

        ///Core
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        [SerializeField, FormerlySerializedAs("resourceStorage")]
        private ResourceStorage storage;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AtomicFunction<bool> isActive;

        public override void Compose()
        {
            base.Compose();
            this.isActive.Compose(() => this.gameObject.activeSelf);
        }

        private void Awake()
        {
            this.Compose();
        }

        private void OnEnable()
        {
            this.storage.OnStateChanged += this.OnStateChanged;
        }

        private void OnDisable()
        {
            this.storage.OnStateChanged -= this.OnStateChanged;
        }

        private void OnStateChanged()
        {
            if (this.storage.IsEmpty())
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.animator.Play(ChopAnimHash, -1, 0);
            }
        }
    }
}