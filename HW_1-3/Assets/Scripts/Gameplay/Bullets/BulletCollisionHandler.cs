using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        private Bullet bullet;

        public BulletCollisionHandler(Bullet bullet)
        {
            this.bullet = bullet;
        }

        public void Enable()
        {
            bullet.OnCollisionEntered += OnBulletCollision;
        }

        public void Disable()
        {
            bullet.OnCollisionEntered -= OnBulletCollision;
        }

        private void OnBulletCollision(Collision2D collision)
        {
            bullet.Despawn();
        }
    }
}