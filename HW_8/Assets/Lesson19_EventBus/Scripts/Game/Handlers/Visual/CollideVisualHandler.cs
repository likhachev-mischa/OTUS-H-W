using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Pipeline.Visual;
using Lessons.Game.Pipeline.Visual.Tasks;
using UnityEngine;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class CollideVisualHandler : BaseHandler<CollideEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public CollideVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(CollideEvent evt)
        {
            PositionComponent sourcePosition = evt.Entity.Get<PositionComponent>();
            PositionComponent targetPosition = evt.Target.Get<PositionComponent>();

            Vector3 offset = (targetPosition.Value - sourcePosition.Value) * 0.5f;
            
            _visualPipeline.AddTask(new MoveVisualTask(evt.Entity, sourcePosition.Value + offset));
            _visualPipeline.AddTask(new MoveVisualTask(evt.Entity, sourcePosition.Value));
        }
    }
}