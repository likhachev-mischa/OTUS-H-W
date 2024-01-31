using System;
using System.Collections.Generic;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Object = UnityEngine.Object;


namespace EcsEngine
{
    public sealed class EntityPool : IDisposable
    {
        private readonly Entity prefab;
        private readonly EcsWorld world;
        private readonly Transform parent;

        private readonly Dictionary<int, Entity> activeEntities;
        private readonly List<Entity> inactiveEntities;

        private readonly Dictionary<int, EntityPool> allEntities;
        public Entity TemplateEntity { get; private set; }

        public EntityPool(Entity prefab, EcsWorld world, Dictionary<int, EntityPool> allEntities, int initialSize = 0,
            Transform parent = default)
        {
            this.prefab = prefab;
            this.world = world;
            this.parent = parent;

            if (parent == default)
            {
                GameObject parentObj = new($"{prefab.name} Pool");
                this.parent = parentObj.transform;
            }

            activeEntities = new Dictionary<int, Entity>(initialSize);
            inactiveEntities = new List<Entity>(initialSize);

            this.allEntities = allEntities;
            CreateTemplateEntity();
            
            for (int i = 0; i < initialSize; ++i)
            {
                Entity entity = Object.Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero), parent);
                entity.gameObject.SetActive(false);
                entity.Initialize(this.world);
                inactiveEntities.Add(entity);
                this.allEntities.Add(entity.Id, this);

            }
        }

        public Entity GetEntity(Vector3 position, Quaternion rotation)
        {
            Entity entity;
            if (inactiveEntities.Count == 0)
            {
                entity = Object.Instantiate(prefab, position, rotation, parent);
                entity.Initialize(this.world);
                activeEntities.Add(entity.Id, entity);

                this.allEntities.Add(entity.Id, this);
                return entity;

            }

            entity = inactiveEntities[0];
            entity.gameObject.SetActive(true);

            inactiveEntities.Remove(entity);

            activeEntities.Add(entity.Id, entity);

            entity.transform.position = position;
            entity.transform.rotation = rotation;

            return entity;
        }

        public void RemoveEntity(Entity entity)
        {
            activeEntities.Remove(entity.Id);
            entity.gameObject.SetActive(false);
            inactiveEntities.Add(entity);
        }

        public void RemoveEntity(int id)
        {
            Entity entity = activeEntities[id];

            activeEntities.Remove(id);
            entity.gameObject.SetActive(false);
            inactiveEntities.Add(entity);
        }

        public void ResetComponent<T>(int entity) where T:struct
        {
            T component = TemplateEntity.GetData<T>();
            Entity currentEntity = activeEntities[entity];
            currentEntity.SetData(component);
        }

        public void Clear()
        {
            foreach ((int key, Entity value) in activeEntities)
            {
                activeEntities[key].gameObject.SetActive(false);
                inactiveEntities.Add(activeEntities[key]);
            }

            activeEntities.Clear();
        }

        public void Dispose()
        {
            Clear();
            for (var i = 0; i < inactiveEntities.Count; i++)
            {
                allEntities.Remove(inactiveEntities[i].Id);
                inactiveEntities[i].Dispose(); 
                Object.Destroy(inactiveEntities[i]);
            }

            inactiveEntities.Clear();
            TemplateEntity.Dispose();
            Object.Destroy(TemplateEntity);
        }

        private void CreateTemplateEntity()
        {
            TemplateEntity = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
            TemplateEntity.name = "Template " + prefab.name;
            TemplateEntity.gameObject.SetActive(false);
            TemplateEntity.Initialize(this.world);
            TemplateEntity.AddData(new TemplateEntityTag());
            TemplateEntity.AddData(new Inactive());
        }
        
    }
}