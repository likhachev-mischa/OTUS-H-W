using DI;
using Events;
using Events.Effects;
using Game.EventBus;
using Pipeline;

namespace Handlers.Turn
{
    public class TurnFinishHandler: BaseHandler<TurnFinishedRequest>
    {
        private TurnPipeline turnPipeline;
        private VisualPipeline visualPipeline;

        [Inject]
        private void Construct(EventBus eventBus, TurnPipeline turnPipeline, VisualPipeline visualPipeline)
        {
            base.Construct(eventBus);
            this.turnPipeline = turnPipeline;
            this.visualPipeline = visualPipeline;
        }

        protected override void HandleEvent(TurnFinishedRequest evt)
        {
           EventBus.RaiseEvent(new TurnFinishedEvent(evt.Source));
        }

    }
}