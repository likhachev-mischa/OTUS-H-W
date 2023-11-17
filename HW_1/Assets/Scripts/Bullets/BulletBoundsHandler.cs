using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletBoundsHandler:MonoBehaviour
    {
        [SerializeField]
        private LevelBounds levelBounds;
        [SerializeField]
        private BulletSystem bulletSystem;
        
        private List<Bullet> bullets = new();
        private List<Bullet> toDestroy = new();
        
        public void Enable(Bullet bullet)
        {
            bullets.Add(bullet);
        }
        
        private void FixedUpdate()
        {
            foreach (var bullet in bullets)
            {
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    toDestroy.Add(bullet);
                } 
            }

            foreach (var bullet in toDestroy)
            {
                this.bulletSystem.DespawnBullet(bullet);
            }
            toDestroy.Clear();
        }

        public void Disable(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}