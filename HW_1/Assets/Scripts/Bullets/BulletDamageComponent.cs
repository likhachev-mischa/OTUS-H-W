using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletDamageComponent
    {

        public void Enable(Bullet bullet)
        {
            bullet.OnCollisionEntered += DealDamage;
        }

        public void Disable(Bullet bullet)
        {
            bullet.OnCollisionEntered -= DealDamage;
        }
        
        internal static void DealDamage(Bullet bullet, Collision2D collision)
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

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }
    }
}