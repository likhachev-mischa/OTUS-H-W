using DI;
using Events;
using Events.Effects;
using Game.EventBus;
using Pipeline;

namespace Handlers
{
    public class ReceiveDamageVisualHandler : BaseHandler<ReceiveDamageEffect>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(ReceiveDamageEffect evt)
        {
            visualPipeline.AddTask(new UpdateStatsTask(evt.Source));
        }
    }
}