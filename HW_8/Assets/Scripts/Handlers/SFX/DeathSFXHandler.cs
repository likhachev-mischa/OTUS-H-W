using DI;
using Events;
using Game.EventBus;
using Pipeline;
using Pipeline.SFXTasks;

namespace Handlers.SFX
{
    public class DeathSFXHandler : BaseHandler<DeathEvent>
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
            visualPipeline.AddTask(new DeathSFXTask(evt.Source));
        }

    }
}