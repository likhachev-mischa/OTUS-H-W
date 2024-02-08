using DI;
using Events;
using Game.EventBus;
using Pipeline;

namespace Handlers.Turn
{
    public class TurnSkipHandler : BaseHandler<TurnSkippedEvent>
    {
        private TurnPipeline turnPipeline;

        [Inject]
        private void Construct(EventBus eventBus, TurnPipeline turnPipeline)
        {
            base.Construct(eventBus);
            this.turnPipeline = turnPipeline;
        }

        protected override void HandleEvent(TurnSkippedEvent evt)
        {
            turnPipeline.SkipTurn();
        }
    }
}