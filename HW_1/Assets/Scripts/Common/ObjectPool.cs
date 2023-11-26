using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private int initialCount;

        private Transform container;
        private GameObject prefab;
        private Transform worldTransform;

        private Queue<T> objectPool = new();
        private List<T> activeObjects = new();

        public List<T> ActiveObjects => activeObjects;

        public ObjectPool(int initialCount, Transform container, GameObject prefab,
            Transform worldTransform)
        {
            this.initialCount = initialCount;
            this.container = container;
            this.prefab = prefab;
            this.worldTransform = worldTransform;
        }

        public void Initialize()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var obj = Object.Instantiate(this.prefab, this.container);
                this.objectPool.Enqueue(obj.GetComponent<T>());
            }
        }

        public bool SpawnObject(out T obj)
        {
            if (!this.objectPool.TryDequeue(out obj))
            {
                return false;
            }

            activeObjects.Add(obj);
            obj.transform.SetParent(this.worldTransform);
            return true;
        }

        public virtual void RemoveObject(T obj)
        {
            if (!activeObjects.Remove(obj))
            {
                return;
            }

            this.objectPool.Enqueue(obj);
            obj.transform.SetParent(this.container);
        }
    }
}