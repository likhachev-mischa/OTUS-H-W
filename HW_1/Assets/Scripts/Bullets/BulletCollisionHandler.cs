using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        
        private BulletFactory bulletFactory;
        
        public BulletCollisionHandler(BulletFactory bulletFactory)
        {
            this.bulletFactory = bulletFactory;
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
            bulletFactory.DespawnBullet(bullet);
        }
        
    }
}