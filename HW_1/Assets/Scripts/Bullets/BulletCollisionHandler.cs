using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        
        private BulletSystem bulletSystem;
        
        public BulletCollisionHandler(BulletSystem bulletSystem)
        {
            this.bulletSystem = bulletSystem;
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
            bulletSystem.DespawnBullet(bullet);
        }
        
    }
}