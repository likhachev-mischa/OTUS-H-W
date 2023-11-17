using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;
        
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;

        [SerializeField]
        private BulletBoundsHandler bulletBoundsHandler;

        private ObjectPool<Bullet> bulletPool;

        private BulletCollisionHandler bulletCollisionHandler;
        private BulletDamageHandler bulletDamageHandler;
        
        private void Awake()
        {
            bulletPool = new ObjectPool<Bullet>(initialCount, container, prefab, worldTransform);
            bulletPool.Initialize();
            bulletCollisionHandler = new BulletCollisionHandler(this);
            bulletDamageHandler = new BulletDamageHandler();
        }
        
        public Bullet SpawnBullet()
        {
            var bullet = bulletPool.SpawnObject();
            bulletDamageHandler.Enable(bullet);
            bulletCollisionHandler.Enable(bullet);
            bulletBoundsHandler.Enable(bullet);
            return bullet;
        }
        
        public void DespawnBullet(Bullet bullet)
        {
            bulletDamageHandler.Disable(bullet);
            bulletCollisionHandler.Disable(bullet);
            bulletBoundsHandler.Disable(bullet);
            bulletPool.RemoveObject(bullet);
        }
        
       
    }
    
}
