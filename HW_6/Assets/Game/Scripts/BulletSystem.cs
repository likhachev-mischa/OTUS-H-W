using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class BulletSystem : IGameUpdateListener , IGameFinishListener
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int bulletAmount = 15;

        private ObjectPool<Bullet> bulletPool;
        private List<Bullet> bullets;

        [Inject]
        private void Construct()
        {
            bulletPool = new ObjectPool<Bullet>(bulletPrefab, bulletAmount);
        }

        public void OnUpdate(float deltaTime)
        {
            bullets = bulletPool.ActiveObjects;
            for (var i = 0; i < bullets.Count; i++)
            {
                bullets[i].OnUpdate(deltaTime);
            }
        }

        public Bullet SpawnBullet()
        {
            Bullet bullet = bulletPool.GetObject();
            bullet.isDead.Value = false;
            bullet.Despawn.Subscribe(DespawnBullet);
            return bullet;
        }

        public void DespawnBullet(MonoBehaviour obj)
        {
            var bullet = (Bullet)obj;
            bullet.Despawn.Unsubscribe(DespawnBullet);
            bulletPool.RemoveObject(bullet);
        }
        
        public void OnFinish()
        {
            bulletPool.Clear();
        }
    }
}