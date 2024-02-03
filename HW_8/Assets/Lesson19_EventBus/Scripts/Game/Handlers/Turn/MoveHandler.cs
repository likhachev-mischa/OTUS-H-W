using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Level;

namespace Lessons.Game.Handlers.Turn
{
    [UsedImplicitly]
    public sealed class MoveHandler : BaseHandler<MoveEvent>
    {
        private readonly LevelMap _levelMap;
        
        public MoveHandler(EventBus eventBus, LevelMap levelMap) : base(eventBus)
        {
            _levelMap = levelMap;
        }

        protected override void HandleEvent(MoveEvent evt)
        {
            CoordinatesComponent coordinatesComponent = evt.Entity.Get<CoordinatesComponent>();
            
            _levelMap.Entities.RemoveEntity(coordinatesComponent.Value);
            _levelMap.Entities.SetEntity(evt.Coordinates, evt.Entity);
            coordinatesComponent.Value = evt.Coordinates;
            
            if (!_levelMap.Tiles.IsWalkable(evt.Coordinates))
            {
                EventBus.RaiseEvent(new DestroyEvent(evt.Entity));
            }
        }
    }
}