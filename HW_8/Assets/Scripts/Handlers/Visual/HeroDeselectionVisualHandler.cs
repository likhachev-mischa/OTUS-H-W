using DI;
using Events;
using Game.EventBus;
using Pipeline;
using UnityEngine;

namespace Handlers
{
    public class HeroDeselectionVisualHandler: BaseHandler<TurnFinishedEvent>
    {
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(TurnFinishedEvent evt)
        {
            visualPipeline.AddTask(new SelectHeroVisualTask(evt.hero,false));
        }
    }
    
}