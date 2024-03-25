using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Sample
{
    public sealed class Axe : MonoBehaviour
    {
        public IAtomicAction HitAction => this.hitAction;
        public IAtomicObservable<IAtomicObject> HitEvent => this.hitEvent;

        [SerializeField]
        private Transform center;

        [SerializeField]
        private float radius;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField, Space]
        private OverlapSphereAction hitAction;

        [SerializeField]
        private AtomicEvent<IAtomicObject> hitEvent;
        
        private void Awake()
        {
            this.hitAction.Compose(
                this.AsFunction(_ => this.center.position),
                this.AsFunction(_ => this.radius),
                this.AsFunction(_ => this.layerMask),
                new AtomicFunction<IAtomicObject, bool>(it => it.Is(ObjectType.Resource)),
                this.hitEvent
            );
        }

        private void OnDestroy()
        {
            this.hitEvent.Dispose();
        }

        private void OnDrawGizmos()
        {
            if (this.center != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(this.center.position, this.radius);
            }
        }
    }
}