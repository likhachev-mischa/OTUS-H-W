using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        
        private BulletManager bulletManager;
        
        public BulletCollisionHandler(BulletManager bulletManager)
        {
            this.bulletManager = bulletManager;
        }
        public void Enable(Bullet bullet)
        {
            bullet.OnCollisionEntered += this.OnBulletCollision;
        }
        public void Disable(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollision;
        }
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            bulletManager.DespawnBullet(bullet);
        }
        
    }
}