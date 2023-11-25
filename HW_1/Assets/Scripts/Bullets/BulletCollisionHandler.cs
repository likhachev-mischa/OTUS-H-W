using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        private Bullet bullet;

        public BulletCollisionHandler(Bullet bullet,BulletManager bulletManager)
        {
            this.bullet = bullet;
        }
        
        public void Enable()
        {
            this.bullet.OnCollisionEntered += this.OnBulletCollision;
            
        }

        public void Disable()
        {
            this.bullet.OnCollisionEntered -= this.OnBulletCollision;
        }

        private void OnBulletCollision(Collision2D collision)
        {
            this.bullet.Despawn();
        }
    }
}