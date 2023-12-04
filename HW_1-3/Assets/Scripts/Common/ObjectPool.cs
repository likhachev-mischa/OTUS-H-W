using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly int initialCount;

        private readonly Transform container;
        private readonly GameObject prefab;
        private readonly Transform worldTransform;

        private readonly Queue<T> objectPool = new();
        private readonly List<T> activeObjects = new();

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
            for (var i = 0; i < initialCount; i++)
            {
                GameObject obj = Object.Instantiate(prefab, container);
                objectPool.Enqueue(obj.GetComponent<T>());
            }
        }

        public bool SpawnObject(out T obj)
        {
            if (!objectPool.TryDequeue(out obj))
            {
                return false;
            }

            activeObjects.Add(obj);
            obj.transform.SetParent(worldTransform);
            return true;
        }

        public void RemoveObject(T obj)
        {
            if (!activeObjects.Remove(obj))
            {
                return;
            }

            objectPool.Enqueue(obj);
            obj.transform.SetParent(container);
        }
    }
}