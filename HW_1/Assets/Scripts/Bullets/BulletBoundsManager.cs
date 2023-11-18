using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class BulletBoundsManager:MonoBehaviour
    {
        [SerializeField]
        private LevelBounds levelBounds;
        [FormerlySerializedAs("bulletSystem")] [SerializeField]
        private BulletFactory bulletFactory;
        
        private List<Bullet> bullets = new();
        private List<Bullet> toDestroy = new();
        
        public void Enable(Bullet bullet)
        {
            bullets.Add(bullet);
        }
        
        private void FixedUpdate()
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
                this.bulletFactory.DespawnBullet(bullet);
            }

            toDestroy.Clear();
        }

        public void Disable(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
    }
}