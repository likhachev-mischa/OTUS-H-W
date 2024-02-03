using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Pipeline.Visual;
using Lessons.Game.Pipeline.Visual.Tasks;
using Lessons.Level;
using UnityEngine;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class MoveVisualHandler : BaseVisualHandler<MoveEvent>
    {
        private readonly LevelMap _levelMap;
        
        public MoveVisualHandler(EventBus eventBus, 
            VisualPipeline visualPipeline, LevelMap levelMap) : base(eventBus, visualPipeline)
        {
            _levelMap = levelMap;
        }

        protected override void HandleEvent(MoveEvent evt)
        {
            Vector3 position = _levelMap.Tiles.CoordinatesToPosition(evt.Coordinates);
            VisualPipeline.AddTask(new MoveVisualTask(evt.Entity, position));
        }
    }
}