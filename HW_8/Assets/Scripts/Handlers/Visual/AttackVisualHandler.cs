
using DI;
using Events;
using Game.EventBus;
using Pipeline;

namespace Handlers
{
    public sealed class AttackVisualHandler: BaseHandler<AttackEvent>
    {
        private VisualPipeline visualPipeline;
        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }
        
        protected override void HandleEvent(AttackEvent evt)
        {
            visualPipeline.AddTask(new AttackVisualTask(evt.Source,evt.Target.entity));
        }
    }
}