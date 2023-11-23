﻿using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletBoundsCorrector : MonoBehaviour,
        IGameFixedUpdateListener
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private BulletManager bulletManager;

        private List<Bullet> bullets = new();
        private List<Bullet> toDestroy = new();

        public void Enable(Bullet bullet)
        {
            bullets.Add(bullet);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (var index = 0; index < bullets.Count; index++)
            {
                var bullet = bullets[index];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    toDestroy.Add(bullet);
                }
            }

            for (var index = 0; index < toDestroy.Count; index++)
            {
                var bullet = toDestroy[index];
                this.bulletManager.DespawnBullet(bullet);
            }

            toDestroy.Clear();
        }

        public void Disable(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}