using DI;
using Events;
using Game.EventBus;
using Pipeline;

namespace Handlers
{
    public class ReceiveDamageVisualHandler : BaseHandler<ReceiveDamageEvent>
    {
        private VisualPipeline visualPipeline;
        
        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }
        
        protected override void HandleEvent(ReceiveDamageEvent evt)
        {
            visualPipeline.AddTask(new UpdateStatsTask(evt.Source));
        }
    }
}