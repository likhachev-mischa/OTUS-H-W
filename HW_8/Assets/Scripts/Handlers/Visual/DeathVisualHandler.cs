using DI;
using Events;
using Game.EventBus;
using Pipeline;

namespace Handlers
{
    public class DeathVisualHandler : BaseHandler<DeathEvent>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(DeathEvent evt)
        {
            visualPipeline.AddTask(new DeathVisualTask(evt.Target.entity));
        }
    }
}