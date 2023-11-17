using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : ObjectPool<Bullet>
    {
        
        private readonly HashSet<Bullet> activeObjects = new();
        public BulletPool(int initialCount, Transform container, Bullet prefab,
            Transform worldTransform, LevelBounds levelBounds)
            : base(initialCount,container,prefab,
            worldTransform,levelBounds){}

        public override Bullet SpawnObject()
        {
            Bullet bullet = base.SpawnObject();
            return bullet;
        }
        public void CheckForBounds()
        {
            this.cache.Clear();
            this.cache.AddRange(this.activeObjects);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var obj = this.cache[i];
                if (!this.levelBounds.InBounds(obj.transform.position))
                {
                    this.RemoveObject(obj);
                }
            }
        }

        public bool TryAddToActive(Bullet bullet)
        {
            return activeObjects.Add(bullet);
        }
        
        public override void RemoveObject(Bullet bullet)
        {
            if (activeObjects.Remove(bullet))
            {
                base.RemoveObject(bullet);
            }
            
        }

    }
    
    
}