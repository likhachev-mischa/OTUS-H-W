using Entities;
using UnityEngine;

namespace Lessons.Game.Events
{
    public readonly struct MoveEvent : IEvent
    {
        public readonly IEntity Entity;
        public readonly Vector2Int Coordinates;

        public MoveEvent(IEntity entity, Vector2Int coordinates)
        {
            Entity = entity;
            Coordinates = coordinates;
        }
    }
}