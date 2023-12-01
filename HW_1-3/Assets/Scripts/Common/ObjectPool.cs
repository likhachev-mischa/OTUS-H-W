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
        private readonly ServiceLocator serviceLocator;

        private readonly Queue<T> objectPool = new();
        private readonly List<T> activeObjects = new();

        public List<T> ActiveObjects => activeObjects;

        public ObjectPool(int initialCount, Transform container, GameObject prefab,
            Transform worldTransform, ServiceLocator serviceLocator)
        {
            this.initialCount = initialCount;
            this.container = container;
            this.prefab = prefab;
            this.worldTransform = worldTransform;
            this.serviceLocator = serviceLocator;
        }

        public void Initialize()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var obj = Object.Instantiate(this.prefab, this.container);
                DependencyInjector.Inject(obj,serviceLocator);
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

        public void RemoveObject(T obj)
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