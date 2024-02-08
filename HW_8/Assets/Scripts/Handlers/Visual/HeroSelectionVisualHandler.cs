using DI;
using Events;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers
{
    public class HeroSelectionVisualHandler : BaseHandler<HeroSelectionEvent>
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
            visualPipeline.AddTask(new SelectHeroVisualTask(evt.hero, true));
            visualPipeline.Run();
        }
    }
}