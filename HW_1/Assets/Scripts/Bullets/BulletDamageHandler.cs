using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletDamageHandler
    {
        public void Enable(Bullet bullet)
        {
            bullet.OnCollisionEntered += DealDamage;
        }

        public void Disable(Bullet bullet)
        {
            bullet.OnCollisionEntered -= DealDamage;
        }

        private static void DealDamage(Bullet bullet, Collision2D collision)
        {
            var other = collision.gameObject;

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