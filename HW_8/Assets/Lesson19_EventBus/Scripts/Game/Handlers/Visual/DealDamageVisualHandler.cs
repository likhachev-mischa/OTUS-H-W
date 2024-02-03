using JetBrains.Annotations;
using Lessons.Entities.Common.Components;
using Lessons.Game.Events;
using Lessons.Game.Pipeline.Visual;
using Lessons.Game.Pipeline.Visual.Tasks;
using UnityEngine;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class DealDamageVisualHandler : BaseHandler<DealDamageEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public DealDamageVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(DealDamageEvent evt)
        {
            Vector3 scale = evt.Entity.Get<TransformComponent>().Value.localScale;
            
            _visualPipeline.AddTask(new ScaleChangeVisualTask(evt.Entity, scale * 2));
            _visualPipeline.AddTask(new ScaleChangeVisualTask(evt.Entity, scale));
        }
    }
}