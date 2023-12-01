
using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletLauncher
    {
        
        private BulletManager bulletManager;

        [Inject]
        private void Construct(BulletManager bulletManager)
        {
            this.bulletManager = bulletManager;
        }
        
        public void LaunchBullet(Bullet.Args args)
        {
            if (!this.bulletManager.SpawnBullet(out var bullet))
            {
                return;
            }
            bullet.Position = args.position;
            bullet.Color = args.color;
            bullet.PhysicsLayer = args.physicsLayer;
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.Velocity = args.velocity;
        }
    }
}