using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionHandler
    {
        private readonly BulletPool bulletPool;
        private readonly BulletDamageComponent bulletDamageComponent;

        public BulletCollisionHandler(BulletPool bulletPool)
        {
            this.bulletPool = bulletPool;
            bulletDamageComponent = new BulletDamageComponent();
        }
        public void Enable(Bullet bullet)
        {
            bulletDamageComponent.Enable(bullet);
            bullet.OnCollisionEntered += this.OnBulletCollision;
        }
        public void Disable(Bullet bullet)
        {
            bulletDamageComponent.Disable(bullet);
            bullet.OnCollisionEntered -= this.OnBulletCollision;
        }
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            bulletPool.RemoveObject(bullet);
            Disable(bullet);
        }
        
    }
}