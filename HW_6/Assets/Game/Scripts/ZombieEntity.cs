using UnityEngine;

namespace Game
{
    public sealed class ZombieEntity : Entity
    {
        private Zombie zombie;
        
        public void Awake()
        {
            zombie = this.gameObject.GetComponent<Zombie>();
            
            Add(new MovementComponent(zombie.Moved, this.transform));
            Add(new RotationComponent(zombie.Rotated));
            Add(new AttackComponent(zombie.AttackDistance,zombie.CanAttack,zombie.AttackRequest,zombie.Target));
            Add(new TakeDamageComponent(zombie.TakeDamage));
        }
    }
}