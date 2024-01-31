using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine
{
    public sealed class EntityPoolRegistry
    {
        private readonly Dictionary<Entity, EntityPool> pools = new();
        private readonly Dictionary<int, EntityPool> entities = new();

        private readonly EcsWorld world;
        
        public EntityPoolRegistry(EcsWorld world)
        {
            this.world = world;
        }
        
        private EntityPool GetPool(Entity prefab)
        {
            if (pools.TryGetValue(prefab, out EntityPool pool))
            {
                return pool;
            }

            EntityPool newPool = new(prefab, world, entities);
            pools.Add(prefab, newPool);

            return newPool;
        }

        public Entity SpawnEntity(Entity prefab, Vector3 position, Quaternion rotation)
        {
            EntityPool pool = GetPool(prefab);
            return pool.GetEntity(position, rotation);
        }

        public bool HasEntity(int entity)
        {
            return entities.ContainsKey(entity);
        }
        
        public void ResetComponent<T>(int entity) where T:struct
        {
            EntityPool pool = entities[entity];
            pool.ResetComponent<T>(entity);
        }

        public void RemoveEntity(int id)
        {
            EntityPool pool = entities[id];
            pool.RemoveEntity(id);
        }
    }
}