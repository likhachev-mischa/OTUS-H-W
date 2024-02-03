using System.Collections.Generic;
using Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Lessons.Level
{
    [UsedImplicitly]
    public sealed class EntityMap
    {
        private readonly Dictionary<Vector2Int, IEntity> _entities = new();

        public bool HasEntity(Vector2Int coordinates)
        {
            return _entities.ContainsKey(coordinates);
        }
        
        public bool TryGetEntity(Vector2Int coordinates, out IEntity entity)
        {
            return _entities.TryGetValue(coordinates, out entity);
        }

        public IEntity GetEntity(Vector2Int coordinates)
        {
            return _entities[coordinates];
        }

        public void RemoveEntity(Vector2Int coordinates)
        {
            _entities.Remove(coordinates);
        }
        
        public void SetEntity(Vector2Int coordinates, IEntity entity)
        {
            _entities[coordinates] = entity;
        }
    }
}