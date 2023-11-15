using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletDamageHandler
    {
        private Bullet bullet;

        public BulletDamageHandler(Bullet bullet)
        {
            this.bullet = bullet;
        }
        
        public void Enable()
        {
            this.bullet.OnCollisionEntered += DealDamage;
        }

        public void Disable()
        {
            this.bullet.OnCollisionEntered -= DealDamage;
        }

        private void DealDamage(Collision2D collision)
        {
            var other = collision.gameObject;

            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (this.bullet.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HealthComponent hitPoints))
            {
                hitPoints.Health -= bullet.Damage;
            }
        }
    }
}