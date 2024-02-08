using DI;
using Events.Effects;
using Game.EventBus;
using Pipeline;
using Pipeline.SFXTasks;

namespace Handlers.SFX
{
    public class UltimateSFXHandler: BaseHandler<UltimateEffect>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(UltimateEffect evt)
        {
           
            visualPipeline.AddTask(new UltimateSFXTask(evt.Source));
        }

    }
}