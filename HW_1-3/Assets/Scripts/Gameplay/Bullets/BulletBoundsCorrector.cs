using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletBoundsCorrector : IGameFixedUpdateListener
    {
        private LevelBounds levelBounds;
        public event Action<Bullet> OnBulletDespawn;

        private List<Bullet> bullets = new();
        private List<Bullet> toDestroy = new();

        [Inject]
        private void Construct(LevelBounds levelBounds)
        {
            this.levelBounds = levelBounds;
        }
        
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
                this.OnBulletDespawn?.Invoke(bullet);
            }

            toDestroy.Clear();
        }

        public void Disable(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}