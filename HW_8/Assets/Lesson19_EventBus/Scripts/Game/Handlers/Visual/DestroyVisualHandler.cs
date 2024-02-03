using JetBrains.Annotations;
using Lessons.Game.Events;
using Lessons.Game.Pipeline.Visual;
using Lessons.Game.Pipeline.Visual.Tasks;

namespace Lessons.Game.Handlers.Visual
{
    [UsedImplicitly]
    public sealed class DestroyVisualHandler : BaseHandler<DestroyEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        
        public DestroyVisualHandler(EventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(DestroyEvent evt)
        {
            _visualPipeline.AddTask(new DestroyVisualTask(evt.Entity));
        }
    }
}