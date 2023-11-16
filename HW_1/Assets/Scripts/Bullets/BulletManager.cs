using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private BulletPool bulletPool;

        private BulletCollisionHandler bulletCollisionHandler;
        private void Awake()
        {
            bulletPool = new BulletPool(initialCount, container, prefab, worldTransform, levelBounds);
            bulletPool.Initialize();
            bulletCollisionHandler = new BulletCollisionHandler(bulletPool);
        }
        
        public void LaunchBullet(BulletManager.Args args)
        {
            Bullet bullet = bulletPool.SpawnObject();
            
            bullet.Position = args.position;
            bullet.Color = args.color;
            bullet.PhysicsLayer = args.physicsLayer;
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.Velocity = args.velocity;

            if (this.bulletPool.TryAddToActive(bullet))
            {
                bulletCollisionHandler.Enable(bullet);
            }
            

        }
        
        private void FixedUpdate()
        {
            bulletPool.CheckForBounds();
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
    
}
