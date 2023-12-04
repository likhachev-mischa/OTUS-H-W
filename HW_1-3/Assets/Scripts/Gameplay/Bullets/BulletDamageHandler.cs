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
            bullet.OnCollisionEntered += DealDamage;
        }

        public void Disable()
        {
            bullet.OnCollisionEntered -= DealDamage;
        }

        private void DealDamage(Collision2D collision)
        {
            GameObject other = collision.gameObject;

            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.IsPlayer == team.IsPlayer)
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