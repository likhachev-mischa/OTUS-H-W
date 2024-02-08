using DI;
using Events;
using Game.EventBus;
using Pipeline;
using Pipeline.SFXTasks;

namespace Handlers.SFX
{
    public class HeroSelectionVFXHandler : BaseHandler<HeroSelectionEvent>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(HeroSelectionEvent evt)
        {
            visualPipeline.AddTask(new SelectHeroSFXTask(evt.hero));
        }
    }
}