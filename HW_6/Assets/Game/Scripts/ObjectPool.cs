using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private int amount;
        private readonly T prefab;
        private readonly Transform container;
        
        public List<T> ActiveObjects { get;}
        private readonly List<T> inactiveObjects;
        
        public ObjectPool(T prefab, int amount = 50)
        {
            this.prefab = prefab;
            this.amount = amount;

            ActiveObjects = new List<T>(this.amount);
            inactiveObjects = new List<T>(this.amount);
            
            GameObject pool = new ("Pool");
            container = pool.transform;

            for (var i = 0; i < this.amount; i++)
            {
                T obj = Object.Instantiate(prefab,container.position,Quaternion.Euler(Vector3.zero), container);
                obj.gameObject.SetActive(false);
                inactiveObjects.Add(obj);
            }
        }

        public T GetObject()
        {
            T result;
            if (inactiveObjects.Count == 0)
            {
                amount++;
                result = Object.Instantiate(prefab,container.position,Quaternion.Euler(Vector3.zero), container);
            }
            else
            {
                result = inactiveObjects[0];
                inactiveObjects.Remove(result);
            }

            result.gameObject.SetActive(true);
            ActiveObjects.Add(result);
            return result;
        }

        public void RemoveObject(T obj)
        {
            obj.gameObject.SetActive(false);
            ActiveObjects.Remove(obj);
            inactiveObjects.Add(obj);
        }

        public void Clear()
        {
            for (var i = 0; i < ActiveObjects.Count; i++)
            {
                ActiveObjects[i].gameObject.SetActive(false);
                inactiveObjects.Add(ActiveObjects[i]);
            }

            ActiveObjects.Clear();
        }
    }
}