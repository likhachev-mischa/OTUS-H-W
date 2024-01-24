using UnityEngine;

namespace Game
{
    public sealed class ZombieEntity : Entity
    {
        public void Initialize(Zombie zombie)
        {
            Add(new MovementComponent(zombie.Moved, this.transform));
            Add(new RotationComponent(zombie.Rotated));
            Add(new AttackComponent(zombie.AttackDistance,zombie.CanAttack,zombie.AttackRequest,zombie.Target));
            Add(new TakeDamageComponent(zombie.TakeDamage));
        }
    }
}