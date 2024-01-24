using UnityEngine;

namespace Game
{
    public sealed class BulletCollisionMechanics
    {
        private readonly IAtomicValue<int> damage;
        private readonly IAtomicAction death;

        public BulletCollisionMechanics(IAtomicValue<int> damage, IAtomicAction death)
        {
            this.damage = damage;
            this.death = death;
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Entity entity))
            {
                if (entity.TryGet(out TakeDamageComponent takeDamageComponent))
                {
                    takeDamageComponent.TakeDamage(damage.Value);
                }
                death.Invoke();
            }
        }
    }
}