using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class ObjectPool<T> where T: MonoBehaviour
    {
        protected int initialCount;
        
        protected Transform container;
        protected T prefab;
        protected Transform worldTransform;
        protected LevelBounds levelBounds;
    
        protected readonly Queue<T> objectPool = new();
        protected readonly List<T> cache = new();
        
        public ObjectPool(int initialCount, Transform container, T prefab, 
            Transform worldTransform, LevelBounds levelBounds)
        {
            this.initialCount = initialCount;
            this.container = container;
            this.prefab = prefab;
            this.worldTransform = worldTransform;
            this.levelBounds = levelBounds;
        }

        public void Initialize()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var obj = Object.Instantiate(this.prefab, this.container);
                this.objectPool.Enqueue(obj);
            }
        }
        public virtual T SpawnObject()
        {
            if (!this.objectPool.TryDequeue(out var obj))
            {
                return null;
            }
            obj.transform.SetParent(this.worldTransform);
            return obj;

        }
        
        public virtual void RemoveObject(T obj)
        {
            obj.transform.SetParent(this.container);
            this.objectPool.Enqueue(obj);
            
        }
    }

}
