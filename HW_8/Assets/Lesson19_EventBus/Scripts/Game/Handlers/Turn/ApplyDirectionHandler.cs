using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Level;
using UnityEngine;

namespace Lessons.Game.Handlers.Turn
{
    [UsedImplicitly]
    public sealed class ApplyDirectionHandler : BaseHandler<ApplyDirectionEvent>
    {
        private readonly LevelMap _levelMap;

        public ApplyDirectionHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void HandleEvent(ApplyDirectionEvent evt)
        {
            CoordinatesComponent coordinates = evt.Entity.Get<CoordinatesComponent>();
            Vector2Int targetCoordinates = coordinates.Value + evt.Direction;

            if (_levelMap.Entities.HasEntity(targetCoordinates))
            {
                EventBus.RaiseEvent(new AttackEvent(evt.Entity, 
                    _levelMap.Entities.GetEntity(targetCoordinates)));
                return;
            }

            if (_levelMap.Tiles.IsWalkable(targetCoordinates))
            {
                EventBus.RaiseEvent(new MoveEvent(evt.Entity, targetCoordinates));
            }
        }
    }
}